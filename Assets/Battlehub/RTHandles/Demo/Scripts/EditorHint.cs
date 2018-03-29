using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTHandles
{
    public class EditorHint : MonoBehaviour
    {
        [SerializeField]
        private EditorDemo EditorDemo;

        // Use this for initialization
        private void Start()
        {
            string vertexSnap = string.Empty;

            Text text = GetComponent<Text>();
            text.text = "Right / Mid Mouse Button or Arrows - scene navigation\n" +
                        "Mouse Wheel - zoom\n" +
                        EditorDemo.FocusKey + " - focus \n" +
                        vertexSnap +
                        EditorDemo.ModifierKey + " + " + EditorDemo.SnapToGridKey + " - snap to grid \n" +
                        EditorDemo.ModifierKey + " + " + EditorDemo.DuplicateKey + " - duplicate object" +
                        EditorDemo.DeleteKey + " - delete object \n" +
                        EditorDemo.ModifierKey + " + " + EditorDemo.EnterPlayModeKey + " - toggle playmode \n" +
                        "Q W E R - select  handle \n" + //hardcoded in SelectionController
                        "X - toggle coordinate system \n" +  //hardcoded in SelectionController
                        "To create prefab click corresponding button";

        }
    }

}
