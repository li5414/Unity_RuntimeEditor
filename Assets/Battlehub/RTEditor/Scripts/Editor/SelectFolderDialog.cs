using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

using UnityObject = UnityEngine.Object;
using Battlehub.RTSaveLoad;

namespace Battlehub.RTEditor
{
    public class SelectFolderDialog : EditorWindow
    {
        public Dictionary<UnityObject, FolderTemplate[]> ObjectsWithMultipleFolders = new Dictionary<UnityObject, FolderTemplate[]>();
        private int m_index;
        private bool m_closed;

        public bool ResourceMapUpdateRequired;

        private void OnGUI()
        {
            if (m_closed)
            {
                return;
            }

            KeyValuePair<UnityObject, FolderTemplate[]> kvp = ObjectsWithMultipleFolders.First();
            UnityObject obj = kvp.Key;
            FolderTemplate[] folders = kvp.Value;
            GUILayout.Label("Select folder for " + obj.name + " " + obj.GetType().Name);

            m_index = EditorGUILayout.Popup(m_index, folders.Select(f => f.name).ToArray());
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select"))
            {
                FolderTemplate folder = folders[m_index];
                
                if (!folder.Objects.Contains(obj))
                {
                    Undo.RecordObject(folder, "BH.RTE.ExposeToEdiro");

                    System.Array.Resize(ref folder.Objects, folder.Objects.Length + 1);
                    folder.Objects[folder.Objects.Length - 1] = obj;
                    
                    EditorUtility.SetDirty(folder);
                }
                
                ObjectsWithMultipleFolders.Remove(obj);
                if (ObjectsWithMultipleFolders == null || ObjectsWithMultipleFolders.Count == 0)
                {
                    Close();
                    if (ResourceMapUpdateRequired)
                    {
                        if(EditorUtility.DisplayDialog("Resouce Map Update required", "Some assets is not included to resource map. Update resource map?", "Yes", "No"))
                        {
                            RuntimeSaveLoadMenu.CreateResourceMap();
                        }
                    }
                    m_closed = true;
                }
            }
            if (GUILayout.Button("Cancel"))
            {
                ObjectsWithMultipleFolders.Remove(obj);
                if (ObjectsWithMultipleFolders == null || ObjectsWithMultipleFolders.Count == 0)
                {
                    Close();
                }
                m_closed = true;
            }
            GUILayout.EndHorizontal();

            
        }
    }

}
