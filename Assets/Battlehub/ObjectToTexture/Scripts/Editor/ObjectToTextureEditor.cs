using UnityEngine;

using System.Collections;
using UnityEditor;

namespace Battlehub.Utils
{
    /// <summary>
    /// http://crappycoding.com/2014/12/create-gameobject-image-using-render-textures/
    /// </summary>
    [CustomEditor(typeof(ObjectToTexture))]
    public class ObjectImageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ObjectToTexture t = (ObjectToTexture)target;
            t.objectImageLayer = EditorGUILayout.LayerField("Object Image Layer", t.objectImageLayer);

            if (GUI.changed)
                EditorUtility.SetDirty(target);

            DrawDefaultInspector();
        }

    }
}
