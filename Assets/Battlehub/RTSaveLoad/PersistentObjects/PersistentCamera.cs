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
	public class PersistentCamera : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Camera o = (UnityEngine.Camera)obj;
			o.fieldOfView = fieldOfView;
			o.nearClipPlane = nearClipPlane;
			o.farClipPlane = farClipPlane;
			o.renderingPath = (UnityEngine.RenderingPath)renderingPath;
			o.allowHDR = allowHDR;
			o.forceIntoRenderTexture = forceIntoRenderTexture;
			o.allowMSAA = allowMSAA;
			o.orthographicSize = orthographicSize;
			o.orthographic = orthographic;
			o.opaqueSortMode = (UnityEngine.Rendering.OpaqueSortMode)opaqueSortMode;
			o.transparencySortMode = (UnityEngine.TransparencySortMode)transparencySortMode;
			o.transparencySortAxis = transparencySortAxis;
			o.depth = depth;
			o.cullingMask = cullingMask;
			o.eventMask = eventMask;
			o.backgroundColor = backgroundColor;
			o.rect = rect;
			o.pixelRect = pixelRect;
			o.targetTexture = (UnityEngine.RenderTexture)objects.Get(targetTexture);
			o.useJitteredProjectionMatrixForTransparentRendering = useJitteredProjectionMatrixForTransparentRendering;
			o.clearFlags = (UnityEngine.CameraClearFlags)clearFlags;
			o.stereoSeparation = stereoSeparation;
			o.stereoConvergence = stereoConvergence;
			o.cameraType = (UnityEngine.CameraType)cameraType;
			//o.stereoMirrorMode = stereoMirrorMode;
			o.stereoTargetEye = (UnityEngine.StereoTargetEyeMask)stereoTargetEye;
			o.targetDisplay = targetDisplay;
			o.useOcclusionCulling = useOcclusionCulling;
			o.layerCullDistances = layerCullDistances;
			o.layerCullSpherical = layerCullSpherical;
			o.depthTextureMode = (UnityEngine.DepthTextureMode)depthTextureMode;
			o.clearStencilAfterLightingPass = clearStencilAfterLightingPass;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Camera o = (UnityEngine.Camera)obj;
			fieldOfView = o.fieldOfView;
			nearClipPlane = o.nearClipPlane;
			farClipPlane = o.farClipPlane;
			renderingPath = (uint)o.renderingPath;
			allowHDR = o.allowHDR;
			forceIntoRenderTexture = o.forceIntoRenderTexture;
			allowMSAA = o.allowMSAA;
			orthographicSize = o.orthographicSize;
			orthographic = o.orthographic;
			opaqueSortMode = (uint)o.opaqueSortMode;
			transparencySortMode = (uint)o.transparencySortMode;
			transparencySortAxis = o.transparencySortAxis;
			depth = o.depth;
			cullingMask = o.cullingMask;
			eventMask = o.eventMask;
			backgroundColor = o.backgroundColor;
			rect = o.rect;
			pixelRect = o.pixelRect;
			targetTexture = o.targetTexture.GetMappedInstanceID();
			useJitteredProjectionMatrixForTransparentRendering = o.useJitteredProjectionMatrixForTransparentRendering;
			clearFlags = (uint)o.clearFlags;
			stereoSeparation = o.stereoSeparation;
			stereoConvergence = o.stereoConvergence;
			cameraType = (uint)o.cameraType;
			//stereoMirrorMode = o.stereoMirrorMode;
			stereoTargetEye = (uint)o.stereoTargetEye;
			targetDisplay = o.targetDisplay;
			useOcclusionCulling = o.useOcclusionCulling;
			layerCullDistances = o.layerCullDistances;
			layerCullSpherical = o.layerCullSpherical;
			depthTextureMode = (uint)o.depthTextureMode;
			clearStencilAfterLightingPass = o.clearStencilAfterLightingPass;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(targetTexture, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Camera o = (UnityEngine.Camera)obj;
			AddDependency(o.targetTexture, dependencies);
		}

		public float fieldOfView;

		public float nearClipPlane;

		public float farClipPlane;

		public uint renderingPath;

		public bool allowHDR;

		public bool forceIntoRenderTexture;

		public bool allowMSAA;

		public float orthographicSize;

		public bool orthographic;

		public uint opaqueSortMode;

		public uint transparencySortMode;

		public UnityEngine.Vector3 transparencySortAxis;

		public float depth;

		public int cullingMask;

		public int eventMask;

		public UnityEngine.Color backgroundColor;

		public UnityEngine.Rect rect;

		public UnityEngine.Rect pixelRect;

		public long targetTexture;

		public bool useJitteredProjectionMatrixForTransparentRendering;

		public uint clearFlags;

		public float stereoSeparation;

		public float stereoConvergence;

		public uint cameraType;

		public bool stereoMirrorMode;

		public uint stereoTargetEye;

		public int targetDisplay;

		public bool useOcclusionCulling;

		public float[] layerCullDistances;

		public bool layerCullSpherical;

		public uint depthTextureMode;

		public bool clearStencilAfterLightingPass;

	}
}
