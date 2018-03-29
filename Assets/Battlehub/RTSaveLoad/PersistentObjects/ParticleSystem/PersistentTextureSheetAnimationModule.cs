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
	#endif
	[System.Serializable]
	public class PersistentTextureSheetAnimationModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.TextureSheetAnimationModule o = (UnityEngine.ParticleSystem.TextureSheetAnimationModule)obj;
			o.enabled = enabled;
			o.numTilesX = numTilesX;
			o.numTilesY = numTilesY;
			o.animation = (UnityEngine.ParticleSystemAnimationType)animation;
			o.useRandomRow = useRandomRow;
			o.frameOverTime = Write(o.frameOverTime, frameOverTime, objects);
			o.frameOverTimeMultiplier = frameOverTimeMultiplier;
			o.startFrame = Write(o.startFrame, startFrame, objects);
			o.startFrameMultiplier = startFrameMultiplier;
			o.cycleCount = cycleCount;
			o.rowIndex = rowIndex;
			o.uvChannelMask = (UnityEngine.Rendering.UVChannelFlags)uvChannelMask;
			o.flipU = flipU;
			o.flipV = flipV;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.TextureSheetAnimationModule o = (UnityEngine.ParticleSystem.TextureSheetAnimationModule)obj;
			enabled = o.enabled;
			numTilesX = o.numTilesX;
			numTilesY = o.numTilesY;
			animation = (uint)o.animation;
			useRandomRow = o.useRandomRow;
			frameOverTime = Read(frameOverTime, o.frameOverTime);
			frameOverTimeMultiplier = o.frameOverTimeMultiplier;
			startFrame = Read(startFrame, o.startFrame);
			startFrameMultiplier = o.startFrameMultiplier;
			cycleCount = o.cycleCount;
			rowIndex = o.rowIndex;
			uvChannelMask = (uint)o.uvChannelMask;
			flipU = o.flipU;
			flipV = o.flipV;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(frameOverTime, dependencies, objects, allowNulls);
			FindDependencies(startFrame, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.TextureSheetAnimationModule o = (UnityEngine.ParticleSystem.TextureSheetAnimationModule)obj;
			GetDependencies(frameOverTime, o.frameOverTime, dependencies);
			GetDependencies(startFrame, o.startFrame, dependencies);
		}

		public bool enabled;

		public int numTilesX;

		public int numTilesY;

		public uint animation;

		public bool useRandomRow;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve frameOverTime;

		public float frameOverTimeMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startFrame;

		public float startFrameMultiplier;

		public int cycleCount;

		public int rowIndex;

		public uint uvChannelMask;

		public float flipU;

		public float flipV;

	}
}
