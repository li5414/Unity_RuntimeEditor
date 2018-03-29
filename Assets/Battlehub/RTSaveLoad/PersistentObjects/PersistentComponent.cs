#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1041, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour))]
	[ProtoInclude(1042, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentOcclusionArea))]
	[ProtoInclude(1043, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentOcclusionPortal))]
	[ProtoInclude(1044, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentMeshFilter))]
	[ProtoInclude(1045, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderer))]
	[ProtoInclude(1046, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentLODGroup))]
	[ProtoInclude(1047, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentWindZone))]
	[ProtoInclude(1048, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTransform))]
	[ProtoInclude(1049, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentParticleSystem))]
	[ProtoInclude(1050, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentRigidbody))]
	[ProtoInclude(1051, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint))]
	[ProtoInclude(1052, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCollider))]
	[ProtoInclude(1053, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentRigidbody2D))]
	[ProtoInclude(1054, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCloth))]
	[ProtoInclude(1055, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTree))]
	[ProtoInclude(1056, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTextMesh))]
	[ProtoInclude(1057, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCanvasGroup))]
	[ProtoInclude(1058, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCanvasRenderer))]
	[ProtoInclude(1059, typeof(Battlehub.RTSaveLoad.PersistentObjects.VR.WSA.PersistentWorldAnchor))]
	#endif
	[System.Serializable]
	public class PersistentComponent : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Component o = (UnityEngine.Component)obj;
			try { o.tag = tag; }
			catch (UnityEngine.UnityException e) { UnityEngine.Debug.LogWarning(e.Message); }
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Component o = (UnityEngine.Component)obj;
			tag = o.tag;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public string tag;

	}
}
