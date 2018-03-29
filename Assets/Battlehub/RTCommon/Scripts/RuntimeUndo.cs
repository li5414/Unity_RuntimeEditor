using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using System.Collections;
using System.Reflection;
using Battlehub.RTSaveLoad;

namespace Battlehub.RTCommon
{
    public delegate bool ApplyCallback(Record record);
    public delegate void PurgeCallback(Record record);

    public class Record
    {
        private object m_state;
        private object m_target;
        private ApplyCallback m_applyCallback;
        private PurgeCallback m_purgeCallback;

        public object Target
        {
            get { return m_target; }
        }

        public object State
        {
            get
            {
                return m_state;
            }
        }

        public Record(object target, object state, ApplyCallback applyCallback, PurgeCallback purgeCallback)
        {
            if (applyCallback == null)
            {
                throw new System.ArgumentNullException("callback");
            }

            m_target = target;
            m_applyCallback = applyCallback;
            m_purgeCallback = purgeCallback;
            if (state != null)
            {
                m_state = state;
            }
        }

        public bool Apply()
        {
            return m_applyCallback(this);
        }

        public void Purge()
        {
            m_purgeCallback(this);
        }
    }

    public class UndoStack<T> : IEnumerable
    {
        private int m_tosIndex;
        private T[] m_buffer;
        private int m_count;
        private int m_totalCount;

        public int Count
        {
            get
            {
                return m_count;
            }
        }

        public bool CanPop
        {
            get { return m_count > 0; }
        }

        public bool CanRestore
        {
            get { return m_count < m_totalCount; }
        }

        public UndoStack(int size)
        {
            if (size == 0)
            {
                throw new System.ArgumentException("size should be greater than 0", "size");
            }
            m_buffer = new T[size];
        }

        public T Push(T item)
        {
            T purge = m_buffer[m_tosIndex];
            m_buffer[m_tosIndex] = item;
            m_tosIndex++;
            m_tosIndex %= m_buffer.Length;
            if (m_count < m_buffer.Length)
            {
                m_count++;
                purge = default(T);
            }
            m_totalCount = m_count;
            return purge;
        }

        public T Restore()
        {
            if (!CanRestore)
            {
                throw new System.InvalidOperationException("nothing to restore");
            }
            if (m_count < m_totalCount)
            {
                m_tosIndex++;
                m_tosIndex %= m_buffer.Length;
                m_count++;
            }
            return Peek();
        }

        public T Peek()
        {
            if (m_count == 0)
            {
                throw new System.InvalidOperationException("Stack is empty");
            }

            int index = m_tosIndex - 1;
            if (index < 0)
            {
                index = m_buffer.Length - 1;
            }

            return m_buffer[index];
        }

        public T Pop()
        {
            if (m_count == 0)
            {
                throw new System.InvalidOperationException("Stack is empty");
            }

            m_count--;
            m_tosIndex--;
            if (m_tosIndex < 0)
            {
                m_tosIndex = m_buffer.Length - 1;
            }
            return m_buffer[m_tosIndex];
        }

        public void Clear()
        {
            m_tosIndex = 0;
            m_count = 0;
            m_totalCount = 0;

            for (int i = 0; i < m_buffer.Length; ++i)
            {
                m_buffer[i] = default(T);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_buffer.GetEnumerator();
        }
    }

    public delegate void RuntimeUndoEventHandler();

    /// <summary>
    /// Class for handling undo and redo operations
    /// </summary>
    public static class RuntimeUndo
    {
        public static event RuntimeUndoEventHandler BeforeUndo;
        public static event RuntimeUndoEventHandler UndoCompleted;
        public static event RuntimeUndoEventHandler BeforeRedo;
        public static event RuntimeUndoEventHandler RedoCompleted;
        public static event RuntimeUndoEventHandler StateChanged;

        private static List<Record> m_group;
        private static UndoStack<Record[]> m_stack;
        private static Stack<UndoStack<Record[]>> m_stacks;

        public const int Limit = 8192;
        
        public static bool Enabled
        {
            get;
            set;
        }

        public static bool CanUndo
        {
            get { return m_stack.CanPop; }
        }

        public static bool CanRedo
        {
            get { return m_stack.CanRestore; }
        }

        static RuntimeUndo()
        {
            Reset();
        }

        public static void Reset()
        {
            Enabled = true;
            m_group = null;
            m_stack = new UndoStack<Record[]>(Limit);
            m_stacks = new Stack<UndoStack<Record[]>>();
        }

        public static bool IsRecording
        {
            get { return m_group != null; }
        }

        public static void BeginRecord()
        {
            if (!Enabled)
            {
                return;
            }

            m_group = new List<Record>();
        }

        public static void EndRecord()
        {
            if (!Enabled)
            {
                return;
            }

            if (m_group != null)
            {
                Record[] purgeItems = m_stack.Push(m_group.ToArray());
                if (purgeItems != null)
                {
                    for (int i = 0; i < purgeItems.Length; ++i)
                    {
                        purgeItems[i].Purge();
                    }
                }

                if (StateChanged != null)
                {
                    StateChanged();
                }
            }
            m_group = null;
        }

        private class BoolState
        {
            public bool value;
            public BoolState(bool v)
            {
                value = v;
            }
        }
        private static void RecordActivateDeactivate(GameObject g, BoolState value)
        {
            RecordObject(g, value,
               record =>
               {
                   GameObject target = (GameObject)record.Target;
                   BoolState activate = (BoolState)record.State;
                   if (target && target.activeSelf != activate.value)
                   {
                       ExposeToEditor exposeToEditor = target.GetComponent<ExposeToEditor>();
                       if (exposeToEditor)
                       {
                           exposeToEditor.MarkAsDestroyed = !activate.value;
                       }
                       else
                       {
                           target.SetActive(activate.value);
                       }
                       return true;
                   }
                   return false;
               },
               record =>
               {
                   BoolState activate = (BoolState)record.State;
                   if (activate.value)
                   {
                       return;
                   }
                   GameObject target = (GameObject)record.Target;
                   if (target)
                   {
                       ExposeToEditor exposeToEditor = target.GetComponent<ExposeToEditor>();
                       if (exposeToEditor)
                       {
                           if (exposeToEditor.MarkAsDestroyed)
                           {
                               Object.DestroyImmediate(target);
                           }
                       }
                       else
                       {
                           if (!target.activeSelf)
                           {
                               Object.DestroyImmediate(target);
                           }
                       }
                   }
               });
        }

        public static void BeginRegisterCreateObject(GameObject g)
        {
            if (!Enabled)
            {
                return;
            }
            RecordActivateDeactivate(g, new BoolState(false));
        }

        public static void RegisterCreatedObject(GameObject g)
        {
            if (!Enabled)
            {
                return;
            }

            ExposeToEditor exposeToEditor = g.GetComponent<ExposeToEditor>();
            if (exposeToEditor)
            {
                exposeToEditor.MarkAsDestroyed = false;
            }
            else
            {
                g.SetActive(true);
            }

            RecordActivateDeactivate(g, new BoolState(true));
        }

        public static void BeginDestroyObject(GameObject g)
        {
            if (!Enabled)
            {
                return;
            }
            RecordActivateDeactivate(g, new BoolState(true));
        }

        public static void DestroyObject(GameObject g)
        {
            if (!Enabled)
            {
                return;
            }

            ExposeToEditor exposeToEditor = g.GetComponent<ExposeToEditor>();
            if (exposeToEditor)
            {
                exposeToEditor.MarkAsDestroyed = true;
            }
            else
            {
                g.SetActive(false);
            }
            RecordActivateDeactivate(g, new BoolState(false));
        }


        private static object GetValue(object target, MemberInfo m)
        {
            PropertyInfo p = m as PropertyInfo;
            if (p != null)
            {
                return p.GetValue(target, null);
            }

            FieldInfo f = m as FieldInfo;
            if (f != null)
            {
                return f.GetValue(target);
            }

            throw new System.ArgumentException("member is not FieldInfo and is not PropertyInfo", "m");
        }

        private static void SetValue(object target, MemberInfo m, object value)
        {
            PropertyInfo p = m as PropertyInfo;
            if (p != null)
            {
                p.SetValue(target, value, null);
                return;
            }

            FieldInfo f = m as FieldInfo;
            if (f != null)
            {
                f.SetValue(target, value);
                return;
            }

            throw new System.ArgumentException("member is not FieldInfo and is not PropertyInfo", "m");
        }

        private static System.Array DuplicateArray(System.Array array)
        {
            System.Array newArray = (System.Array)System.Activator.CreateInstance(array.GetType(), array.Length);
            if (array != null)
            {
                for (int i = 0; i < newArray.Length; ++i)
                {
                    newArray.SetValue(array.GetValue(i), i);
                }
            }

            return array;
        }


        public static void RecordValue(object target, MemberInfo memberInfo)
        {
            // Debug.Log("Record Value " + target + " " + memberInfo.Name);
            if (!Enabled)
            {
                return;
            }

            if (!(memberInfo is PropertyInfo) && !(memberInfo is FieldInfo))
            {
                Debug.LogWarning("Unable to record value");
                return;
            }

            object val = GetValue(target, memberInfo);
            if (val != null)
            {
                if (val is System.Array)
                {
                    object duplicate = DuplicateArray((System.Array)val);
                    SetValue(target, memberInfo, duplicate);
                }
            }

            RecordObject(target, val, record =>
            {
                object obj = record.Target;
                if (obj == null)
                {
                    return false;
                }

                if (obj is Object)
                {
                    if (((Object)obj) == null)
                    {
                        return false;
                    }
                }

                object state = record.State;
                object value = GetValue(obj, memberInfo);

                bool hasChanged = true;
                if (state == null && value == null)
                {
                    hasChanged = false;
                }
                else if (state != null && value != null)
                {
                    if (state is IEnumerable<object>)
                    {
                        IEnumerable<object> eState = (IEnumerable<object>)state;
                        IEnumerable<object> eValue = (IEnumerable<object>)value;
                        hasChanged = !eState.SequenceEqual(eValue);
                    }
                    else
                    {
                        hasChanged = !state.Equals(value);
                    }

                }

                if (hasChanged)
                {
                    SetValue(obj, memberInfo, state);
                }
                return hasChanged;
            },
            record => { });
        }

        private class TransformRecord
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
            public Transform parent;
            public int siblingIndex = -1;
        }

        public static void RecordTransform(Transform target, Transform parent = null, int siblingIndex = -1)
        {
            if (!Enabled)
            {
                return;
            }

            TransformRecord transformRecord = new TransformRecord { position = target.position, rotation = target.rotation, scale = target.localScale };

            transformRecord.parent = parent;
            transformRecord.siblingIndex = siblingIndex;

            RecordObject(target, transformRecord , record =>
            {
                Transform transform = (Transform)record.Target;
                if (!transform)
                {
                    return false;
                }

                TransformRecord state = (TransformRecord)record.State;
                bool hasChanged = transform.position != state.position ||
                                  transform.rotation != state.rotation ||
                                  transform.localScale != state.scale;

                bool trsOnly = state.siblingIndex == -1;
                if(!trsOnly)
                {
                    hasChanged = hasChanged || transform.parent != state.parent || transform.GetSiblingIndex() != state.siblingIndex;
                }

                if (hasChanged)
                {
                    Transform prevParent = transform.parent;
                    if (!trsOnly)
                    {
                        transform.SetParent(state.parent, true);
                        transform.SetSiblingIndex(state.siblingIndex);
                    }

                    transform.position = state.position;
                    transform.rotation = state.rotation;
                    transform.localScale = state.scale;
                }
                return hasChanged;
            },
            record => { });
        }


        private class SelectionRecord
        {
            public Object[] objects;
            public Object activeObject;
        }

        private class RTSelectionInternalsAccessor : RuntimeSelection
        {
            public static Object activeObjectProperty
            {
                get { return m_activeObject; }
                set
                {
                    m_activeObject = value;
                }
            }

            public static Object[] objectsProperty
            {
                get { return m_objects; }
                set
                {
                    SetObjects(value);
                }
            }
        }

        public static void RecordSelection()
        {
            if (!Enabled)
            {
                return;
            }

            RecordObject(null, new SelectionRecord { objects = RuntimeSelection.objects, activeObject = RuntimeSelection.activeObject }, record =>
            {
                SelectionRecord state = (SelectionRecord)record.State;

                bool hasChanged = false;
                if (state.objects != null && RuntimeSelection.objects != null)
                {
                    if (state.objects.Length != RuntimeSelection.objects.Length)
                    {
                        hasChanged = true;
                    }
                    else
                    {
                        for (int i = 0; i < RuntimeSelection.objects.Length; ++i)
                        {
                            if (state.objects[i] != RuntimeSelection.objects[i])
                            {
                                hasChanged = true;
                                break;
                            }
                        }
                    }
                }
                else if (state.objects == null)
                {
                    hasChanged = RuntimeSelection.objects != null && RuntimeSelection.objects.Length != 0;
                }
                else if (RuntimeSelection.objects == null)
                {
                    hasChanged = state.objects != null && state.objects.Length != 0;
                }

              
                if (hasChanged)
                {
                    List<Object> selection = null;
                    if (state.objects != null)
                    {
                        selection = state.objects.ToList();
                        if (state.activeObject != null)
                        {
                            if (selection.Contains(state.activeObject))
                            {
                                selection.Remove(state.activeObject);
                                selection.Insert(0, state.activeObject);
                            }
                            else
                            {
                                selection.Insert(0, state.activeObject);
                            }
                        }
                        RTSelectionInternalsAccessor.activeObjectProperty = state.activeObject;
                        RTSelectionInternalsAccessor.objectsProperty = selection.ToArray();
                    }
                    else
                    {
                        RTSelectionInternalsAccessor.activeObjectProperty = null;
                        RTSelectionInternalsAccessor.objectsProperty = null;
                    }
                }
                return hasChanged;
            },
            r => { });

        }

        public static void RecordComponent(MonoBehaviour component)
        {
            System.Type type = component.GetType();
            if(type == typeof(MonoBehaviour))
            {
                return;
            }

            List<FieldInfo> serializableFields = new List<FieldInfo>();
            while(type != typeof(MonoBehaviour))
            {
                serializableFields.AddRange(type.GetSerializableFields());
                type = type.BaseType();
            }

            
            bool endRecord = false;
            if(!IsRecording)
            {
                endRecord = true;
                BeginRecord();
            }
            
            for(int i = 0; i < serializableFields.Count; ++i)
            {
                RecordValue(component, serializableFields[i]);
            }

            if(endRecord)
            {
                EndRecord();
            }
        }

        public static void RecordObject(object target, object state, ApplyCallback applyCallback, PurgeCallback purgeCallback = null)
        {
            if (!Enabled)
            {
                return;
            }

            if(purgeCallback == null)
            {
                purgeCallback = record => { };
            }

            if (m_group != null)
            {
                m_group.Add(new Record(target, state, applyCallback, purgeCallback));
            }
            else
            {
                Record[] purgeItems = m_stack.Push(new[] { new Record(target, state, applyCallback, purgeCallback) });
                if (purgeItems != null)
                {
                    for (int i = 0; i < purgeItems.Length; ++i)
                    {
                        purgeItems[i].Purge();
                    }
                }

                if (StateChanged != null)
                {
                    StateChanged();
                }
            }
        }

       

        public static void Redo()
        {
            if (!Enabled)
            {
                return;
            }

            if (!m_stack.CanRestore)
            {
                return;
            }

            if (BeforeRedo != null)
            {
                BeforeRedo();
            }

            bool somethingHasChanged;
            do
            {
                somethingHasChanged = false;
                Record[] records = m_stack.Restore();
                for (int i = 0; i < records.Length; ++i)
                {
                    Record record = records[i];
                    somethingHasChanged |= record.Apply();
                }
            }
            while (!somethingHasChanged && m_stack.CanRestore);

            if (RedoCompleted != null)
            {
                RedoCompleted();
            }
        }

        public static void Undo()
        {
            if (!Enabled)
            {
                return;
            }

            if (!m_stack.CanPop)
            {
                return;
            }

            if (BeforeUndo != null)
            {
                BeforeUndo();
            }

            bool somethingHasChanged;
            do
            {
                somethingHasChanged = false;
                Record[] records = m_stack.Pop();
                for (int i = 0; i < records.Length; ++i)
                {
                    Record record = records[i];
                    somethingHasChanged |= record.Apply();
                }
            }
            while (!somethingHasChanged && m_stack.CanPop);

            if (UndoCompleted != null)
            {
                UndoCompleted();
            }
        }

        public static void Purge()
        {
            if (!Enabled)
            {
                return;
            }

            foreach (Record[] records in m_stack)
            {
                if (records != null)
                {
                    for (int i = 0; i < records.Length; ++i)
                    {
                        Record record = records[i];
                        record.Purge();
                    }
                }
            }
            m_stack.Clear();

            if (StateChanged != null)
            {
                StateChanged();
            }
        }

        public static void Store()
        {
            if (!Enabled)
            {
                return;
            }
            m_stacks.Push(m_stack);
            m_stack = new UndoStack<Record[]>(Limit);
            if (StateChanged != null)
            {
                StateChanged();
            }
        }

        public static void Restore()
        {
            if (!Enabled)
            {
                return;
            }

            if (m_stack != null)
            {
                m_stack.Clear();
            }

            if(m_stacks.Count > 0)
            {
                m_stack = m_stacks.Pop();
                if (StateChanged != null)
                {
                    StateChanged();
                }
            }
        }

        
    }
}
