#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.Video
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentVideoPlayer : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Video.VideoPlayer o = (UnityEngine.Video.VideoPlayer)obj;
			o.source = (UnityEngine.Video.VideoSource)source;
			o.url = url;
			o.clip = (UnityEngine.Video.VideoClip)objects.Get(clip);
			o.renderMode = (UnityEngine.Video.VideoRenderMode)renderMode;
			o.targetCamera = (UnityEngine.Camera)objects.Get(targetCamera);
			o.targetTexture = (UnityEngine.RenderTexture)objects.Get(targetTexture);
			o.targetMaterialRenderer = (UnityEngine.Renderer)objects.Get(targetMaterialRenderer);
			o.targetMaterialProperty = targetMaterialProperty;
			o.aspectRatio = (UnityEngine.Video.VideoAspectRatio)aspectRatio;
			o.targetCameraAlpha = targetCameraAlpha;
			o.waitForFirstFrame = waitForFirstFrame;
			o.playOnAwake = playOnAwake;
			o.time = time;
			o.frame = frame;
			o.playbackSpeed = playbackSpeed;
			o.isLooping = isLooping;
			o.timeSource = (UnityEngine.Video.VideoTimeSource)timeSource;
			o.skipOnDrop = skipOnDrop;
			o.controlledAudioTrackCount = controlledAudioTrackCount;
			o.audioOutputMode = (UnityEngine.Video.VideoAudioOutputMode)audioOutputMode;
			o.sendFrameReadyEvents = sendFrameReadyEvents;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Video.VideoPlayer o = (UnityEngine.Video.VideoPlayer)obj;
			source = (uint)o.source;
			url = o.url;
			clip = o.clip.GetMappedInstanceID();
			renderMode = (uint)o.renderMode;
			targetCamera = o.targetCamera.GetMappedInstanceID();
			targetTexture = o.targetTexture.GetMappedInstanceID();
			targetMaterialRenderer = o.targetMaterialRenderer.GetMappedInstanceID();
			targetMaterialProperty = o.targetMaterialProperty;
			aspectRatio = (uint)o.aspectRatio;
			targetCameraAlpha = o.targetCameraAlpha;
			waitForFirstFrame = o.waitForFirstFrame;
			playOnAwake = o.playOnAwake;
			time = o.time;
			frame = o.frame;
			playbackSpeed = o.playbackSpeed;
			isLooping = o.isLooping;
			timeSource = (uint)o.timeSource;
			skipOnDrop = o.skipOnDrop;
			controlledAudioTrackCount = o.controlledAudioTrackCount;
			audioOutputMode = (uint)o.audioOutputMode;
			sendFrameReadyEvents = o.sendFrameReadyEvents;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(clip, dependencies, objects, allowNulls);
			AddDependency(targetCamera, dependencies, objects, allowNulls);
			AddDependency(targetTexture, dependencies, objects, allowNulls);
			AddDependency(targetMaterialRenderer, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Video.VideoPlayer o = (UnityEngine.Video.VideoPlayer)obj;
			AddDependency(o.clip, dependencies);
			AddDependency(o.targetCamera, dependencies);
			AddDependency(o.targetTexture, dependencies);
			AddDependency(o.targetMaterialRenderer, dependencies);
		}

		public uint source;

		public string url;

		public long clip;

		public uint renderMode;

		public long targetCamera;

		public long targetTexture;

		public long targetMaterialRenderer;

		public string targetMaterialProperty;

		public uint aspectRatio;

		public float targetCameraAlpha;

		public bool waitForFirstFrame;

		public bool playOnAwake;

		public double time;

		public long frame;

		public float playbackSpeed;

		public bool isLooping;

		public uint timeSource;

		public bool skipOnDrop;

		public ushort controlledAudioTrackCount;

		public uint audioOutputMode;

		public bool sendFrameReadyEvents;

	}
}
