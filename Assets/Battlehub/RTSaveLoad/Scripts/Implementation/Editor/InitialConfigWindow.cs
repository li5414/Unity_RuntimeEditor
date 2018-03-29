using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Battlehub.RTSaveLoad
{
    public class ConfiguratorWindow : EditorWindow
    {
     
        [InitializeOnLoadMethod]
        public static void AutoShow()
        {
            bool firstRun = !EditorPrefs.HasKey("Battlehub.RTSaveLoad.Configurator");
            EditorPrefs.SetBool("Battlehub.RTSaveLoad.Configurator", true);
            if (firstRun)
            {
                ShowWindow();
            }
        }

        //[MenuItem("Tools/Runtime SaveLoad/Configurator")]
        public static void ShowMenuItem()
        {
            ShowWindow();
        }

        public static void ShowWindow()
        {
            ConfiguratorWindow prevWindow = GetWindow<ConfiguratorWindow>();
            if (prevWindow != null)
            {
                prevWindow.Close();
            }

            ConfiguratorWindow window = CreateInstance<ConfiguratorWindow>();
            window.titleContent = new GUIContent("RT Save&Load Config");
            window.ShowUtility();
            window.position = new Rect(200, 200, 355, 210);

        }

        private Texture2D m_Logo;

        private void Awake()
        {
            m_Logo = (Texture2D)Resources.Load("RTSaveLoadLogo", typeof(Texture2D));
            
        }

        private void Start()
        {
            
        }
       
        private void OnGUI()
        {
         


            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            GUILayout.Label(m_Logo);

            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical();
            EditorGUILayout.Separator();
            
            EditorStyles.label.wordWrap = true;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            
            EditorGUILayout.LabelField("Save&Load subsystem require ResourceMap to work property");

            EditorStyles.label.fontStyle = FontStyle.Normal;
            EditorGUILayout.LabelField("NOTE: You will need to update resource map each time you add new resources to project.");
            EditorGUILayout.LabelField("To update resource map use Tools->Runtime SaveLoad->CreateResourceMap menu item.");


            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();
            if (GUILayout.Button("Create Resource Map"))
            {    
                if (Application.isPlaying)
                {
                    EditorUtility.DisplayDialog("Unable to create Resource Map", "Application.isPlaying == true", "OK");
                }
                else
                {
                    ResourceMapGen.CreateResourceMap(true);
                    Close();
                }
            }

        }
    }
}
