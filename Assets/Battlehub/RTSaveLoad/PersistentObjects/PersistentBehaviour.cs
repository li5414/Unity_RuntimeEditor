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
	[ProtoInclude(1004, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCamera))]
	[ProtoInclude(1005, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentFlareLayer))]
	[ProtoInclude(1006, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentLensFlare))]
	[ProtoInclude(1007, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentProjector))]
	[ProtoInclude(1008, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSkybox))]
	[ProtoInclude(1009, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIElement))]
	[ProtoInclude(1010, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentGUILayer))]
	[ProtoInclude(1011, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentLight))]
	[ProtoInclude(1012, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentLightProbeGroup))]
	[ProtoInclude(1013, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentLightProbeProxyVolume))]
	[ProtoInclude(1014, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentMonoBehaviour))]
	[ProtoInclude(1015, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentNetworkView))]
	[ProtoInclude(1016, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentReflectionProbe))]
	[ProtoInclude(1017, typeof(Battlehub.RTSaveLoad.PersistentObjects.Rendering.PersistentSortingGroup))]
	[ProtoInclude(1018, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentConstantForce))]
	[ProtoInclude(1019, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint2D))]
	[ProtoInclude(1020, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCollider2D))]
	[ProtoInclude(1021, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentPhysicsUpdateBehaviour2D))]
	[ProtoInclude(1022, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentEffector2D))]
	[ProtoInclude(1023, typeof(Battlehub.RTSaveLoad.PersistentObjects.AI.PersistentNavMeshAgent))]
	[ProtoInclude(1024, typeof(Battlehub.RTSaveLoad.PersistentObjects.AI.PersistentNavMeshObstacle))]
	[ProtoInclude(1025, typeof(Battlehub.RTSaveLoad.PersistentObjects.AI.PersistentOffMeshLink))]
	[ProtoInclude(1026, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioSource))]
	[ProtoInclude(1027, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioLowPassFilter))]
	[ProtoInclude(1028, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioHighPassFilter))]
	[ProtoInclude(1029, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioReverbFilter))]
	[ProtoInclude(1030, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioBehaviour))]
	[ProtoInclude(1031, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioListener))]
	[ProtoInclude(1032, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioReverbZone))]
	[ProtoInclude(1033, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioDistortionFilter))]
	[ProtoInclude(1034, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioEchoFilter))]
	[ProtoInclude(1035, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAudioChorusFilter))]
	[ProtoInclude(1036, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimator))]
	[ProtoInclude(1037, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimation))]
	[ProtoInclude(1038, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTerrain))]
	[ProtoInclude(1039, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCanvas))]
	[ProtoInclude(1040, typeof(Battlehub.RTSaveLoad.PersistentObjects.Video.PersistentVideoPlayer))]
	#endif
	[System.Serializable]
	public class PersistentBehaviour : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Behaviour o = (UnityEngine.Behaviour)obj;
			o.enabled = enabled;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Behaviour o = (UnityEngine.Behaviour)obj;
			enabled = o.enabled;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool enabled;

	}
}
