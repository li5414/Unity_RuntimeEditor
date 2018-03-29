using System; 
using UnityEngine; 
using System.Collections.Generic; 
namespace Battlehub.RTEditor
{
	public partial class EditorsMap
	{
		protected static void InitEditorsMap()
		{
			m_map = new Dictionary<Type, EditorDescriptor>
			{
				{ typeof(UnityEngine.GameObject), new EditorDescriptor(0, true, false) },
				{ typeof(System.Object), new EditorDescriptor(1, true, true) },
				{ typeof(UnityEngine.Object), new EditorDescriptor(2, true, true) },
				{ typeof(System.Boolean), new EditorDescriptor(3, true, true) },
				{ typeof(System.Enum), new EditorDescriptor(4, true, true) },
				{ typeof(System.Collections.Generic.List<>), new EditorDescriptor(5, true, true) },
				{ typeof(System.Array), new EditorDescriptor(6, true, true) },
				{ typeof(System.String), new EditorDescriptor(7, true, true) },
				{ typeof(System.Int32), new EditorDescriptor(8, true, true) },
				{ typeof(System.Single), new EditorDescriptor(9, true, true) },
				{ typeof(Battlehub.RTEditor.Range), new EditorDescriptor(10, true, true) },
				{ typeof(UnityEngine.Vector2), new EditorDescriptor(11, true, true) },
				{ typeof(UnityEngine.Vector3), new EditorDescriptor(12, true, true) },
				{ typeof(UnityEngine.Vector4), new EditorDescriptor(13, true, true) },
				{ typeof(UnityEngine.Quaternion), new EditorDescriptor(14, true, true) },
				{ typeof(UnityEngine.Color), new EditorDescriptor(15, true, true) },
				{ typeof(UnityEngine.Bounds), new EditorDescriptor(16, true, true) },
				{ typeof(UnityEngine.Component), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.BoxCollider), new EditorDescriptor(18, true, false) },
				{ typeof(UnityEngine.Camera), new EditorDescriptor(17, false, false) },
				{ typeof(UnityEngine.CapsuleCollider), new EditorDescriptor(18, true, false) },
				{ typeof(UnityEngine.FixedJoint), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.HingeJoint), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.Light), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.MeshCollider), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.MeshFilter), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.MeshRenderer), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.MonoBehaviour), new EditorDescriptor(17, false, false) },
				{ typeof(UnityEngine.Rigidbody), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.SkinnedMeshRenderer), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.SphereCollider), new EditorDescriptor(18, true, false) },
				{ typeof(UnityEngine.SpringJoint), new EditorDescriptor(17, true, false) },
				{ typeof(UnityEngine.Transform), new EditorDescriptor(19, true, false) },
				{ typeof(Battlehub.Cubeman.CubemanCharacter), new EditorDescriptor(17, true, false) },
				{ typeof(Battlehub.Cubeman.CubemanUserControl), new EditorDescriptor(17, true, false) },
				//{ typeof(Battlehub.RTEditor.Ev4D_ObjectInfos), new EditorDescriptor(17, false, false) },
			};
		}
	}
}
