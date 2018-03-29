#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentAnimator : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Animator o = (UnityEngine.Animator)obj;
			o.rootPosition = rootPosition;
			o.rootRotation = rootRotation;
			o.applyRootMotion = applyRootMotion;
			o.linearVelocityBlending = linearVelocityBlending;
			o.updateMode = (UnityEngine.AnimatorUpdateMode)updateMode;
			o.stabilizeFeet = stabilizeFeet;
			o.feetPivotActive = feetPivotActive;
			o.speed = speed;
			o.cullingMode = (UnityEngine.AnimatorCullingMode)cullingMode;
			o.recorderStartTime = recorderStartTime;
			o.recorderStopTime = recorderStopTime;
			o.runtimeAnimatorController = (UnityEngine.RuntimeAnimatorController)objects.Get(runtimeAnimatorController);
			o.avatar = (UnityEngine.Avatar)objects.Get(avatar);
			o.layersAffectMassCenter = layersAffectMassCenter;
			o.logWarnings = logWarnings;
			o.fireEvents = fireEvents;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Animator o = (UnityEngine.Animator)obj;
			rootPosition = o.rootPosition;
			rootRotation = o.rootRotation;
			applyRootMotion = o.applyRootMotion;
			linearVelocityBlending = o.linearVelocityBlending;
			updateMode = (uint)o.updateMode;
			stabilizeFeet = o.stabilizeFeet;
			feetPivotActive = o.feetPivotActive;
			speed = o.speed;
			cullingMode = (uint)o.cullingMode;
			recorderStartTime = o.recorderStartTime;
			recorderStopTime = o.recorderStopTime;
			runtimeAnimatorController = o.runtimeAnimatorController.GetMappedInstanceID();
			avatar = o.avatar.GetMappedInstanceID();
			layersAffectMassCenter = o.layersAffectMassCenter;
			logWarnings = o.logWarnings;
			fireEvents = o.fireEvents;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(runtimeAnimatorController, dependencies, objects, allowNulls);
			AddDependency(avatar, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Animator o = (UnityEngine.Animator)obj;
			AddDependency(o.runtimeAnimatorController, dependencies);
			AddDependency(o.avatar, dependencies);
		}

		public UnityEngine.Vector3 rootPosition;

		public UnityEngine.Quaternion rootRotation;

		public bool applyRootMotion;

		public bool linearVelocityBlending;

		public uint updateMode;

		public bool stabilizeFeet;

		public float feetPivotActive;

		public float speed;

		public uint cullingMode;

		public float recorderStartTime;

		public float recorderStopTime;

		public long runtimeAnimatorController;

		public long avatar;

		public bool layersAffectMassCenter;

		public bool logWarnings;

		public bool fireEvents;

	}
}
