#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using System.Runtime.Serialization;
using UnityEngine.Playables;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad
{
	public static partial class SerializationSurrogates
	{
		static SerializationSurrogates()
		{
			m_surrogates.Add(typeof(UnityEngine.GradientAlphaKey), new Battlehub.RTSaveLoad.UnityEngineNS.GradientAlphaKeySurrogate());
			m_surrogates.Add(typeof(UnityEngine.GradientColorKey), new Battlehub.RTSaveLoad.UnityEngineNS.GradientColorKeySurrogate());
			m_surrogates.Add(typeof(UnityEngine.LayerMask), new Battlehub.RTSaveLoad.UnityEngineNS.LayerMaskSurrogate());
			m_surrogates.Add(typeof(UnityEngine.RectOffset), new Battlehub.RTSaveLoad.UnityEngineNS.RectOffsetSurrogate());
			m_surrogates.Add(typeof(UnityEngine.UI.AnimationTriggers), new Battlehub.RTSaveLoad.UnityEngineNS.UINS.AnimationTriggersSurrogate());
			m_surrogates.Add(typeof(UnityEngine.UI.ColorBlock), new Battlehub.RTSaveLoad.UnityEngineNS.UINS.ColorBlockSurrogate());
			m_surrogates.Add(typeof(UnityEngine.AI.NavMeshPath), new Battlehub.RTSaveLoad.UnityEngineNS.AINS.NavMeshPathSurrogate());
			m_surrogates.Add(typeof(UnityEngine.ClothSkinningCoefficient), new Battlehub.RTSaveLoad.UnityEngineNS.ClothSkinningCoefficientSurrogate());
			m_surrogates.Add(typeof(UnityEngine.BoneWeight), new Battlehub.RTSaveLoad.UnityEngineNS.BoneWeightSurrogate());
			m_surrogates.Add(typeof(UnityEngine.TreeInstance), new Battlehub.RTSaveLoad.UnityEngineNS.TreeInstanceSurrogate());
			m_surrogates.Add(typeof(UnityEngine.CharacterInfo), new Battlehub.RTSaveLoad.UnityEngineNS.CharacterInfoSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Vector3), new Battlehub.RTSaveLoad.UnityEngineNS.Vector3Surrogate());
			m_surrogates.Add(typeof(UnityEngine.Color), new Battlehub.RTSaveLoad.UnityEngineNS.ColorSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Rect), new Battlehub.RTSaveLoad.UnityEngineNS.RectSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Matrix4x4), new Battlehub.RTSaveLoad.UnityEngineNS.Matrix4x4Surrogate());
			m_surrogates.Add(typeof(UnityEngine.SceneManagement.Scene), new Battlehub.RTSaveLoad.UnityEngineNS.SceneManagementNS.SceneSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Bounds), new Battlehub.RTSaveLoad.UnityEngineNS.BoundsSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Vector4), new Battlehub.RTSaveLoad.UnityEngineNS.Vector4Surrogate());
#if !UNITY_WINRT && !UNITY_WEBGL
            m_surrogates.Add(typeof(UnityEngine.NetworkViewID), new Battlehub.RTSaveLoad.UnityEngineNS.NetworkViewIDSurrogate());
			m_surrogates.Add(typeof(UnityEngine.NetworkPlayer), new Battlehub.RTSaveLoad.UnityEngineNS.NetworkPlayerSurrogate());
#endif
            m_surrogates.Add(typeof(UnityEngine.Vector2), new Battlehub.RTSaveLoad.UnityEngineNS.Vector2Surrogate());
			m_surrogates.Add(typeof(UnityEngine.RenderBuffer), new Battlehub.RTSaveLoad.UnityEngineNS.RenderBufferSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Quaternion), new Battlehub.RTSaveLoad.UnityEngineNS.QuaternionSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointMotor), new Battlehub.RTSaveLoad.UnityEngineNS.JointMotorSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointLimits), new Battlehub.RTSaveLoad.UnityEngineNS.JointLimitsSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointSpring), new Battlehub.RTSaveLoad.UnityEngineNS.JointSpringSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointDrive), new Battlehub.RTSaveLoad.UnityEngineNS.JointDriveSurrogate());
			m_surrogates.Add(typeof(UnityEngine.SoftJointLimitSpring), new Battlehub.RTSaveLoad.UnityEngineNS.SoftJointLimitSpringSurrogate());
			m_surrogates.Add(typeof(UnityEngine.SoftJointLimit), new Battlehub.RTSaveLoad.UnityEngineNS.SoftJointLimitSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointMotor2D), new Battlehub.RTSaveLoad.UnityEngineNS.JointMotor2DSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointAngleLimits2D), new Battlehub.RTSaveLoad.UnityEngineNS.JointAngleLimits2DSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointTranslationLimits2D), new Battlehub.RTSaveLoad.UnityEngineNS.JointTranslationLimits2DSurrogate());
			m_surrogates.Add(typeof(UnityEngine.JointSuspension2D), new Battlehub.RTSaveLoad.UnityEngineNS.JointSuspension2DSurrogate());
			m_surrogates.Add(typeof(UnityEngine.WheelFrictionCurve), new Battlehub.RTSaveLoad.UnityEngineNS.WheelFrictionCurveSurrogate());
			m_surrogates.Add(typeof(UnityEngine.AI.OffMeshLinkData), new Battlehub.RTSaveLoad.UnityEngineNS.AINS.OffMeshLinkDataSurrogate());
			m_surrogates.Add(typeof(PlayableGraph), new Battlehub.RTSaveLoad.UnityEngineNS.ExperimentalNS.DirectorNS.PlayableGraphSurrogate());
			m_surrogates.Add(typeof(UnityEngine.Color32), new Battlehub.RTSaveLoad.UnityEngineNS.Color32Surrogate());
		}
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class GradientAlphaKeySurrogate : ISerializationSurrogate
#else
	public class GradientAlphaKeySurrogate
#endif
	{
		public System.Single alpha;
		public System.Single time;
		public static implicit operator UnityEngine.GradientAlphaKey(GradientAlphaKeySurrogate v)
		{
			UnityEngine.GradientAlphaKey o =  new UnityEngine.GradientAlphaKey();
			o.alpha = v.alpha;
			o.time = v.time;
			return o;
		}
		public static implicit operator GradientAlphaKeySurrogate(UnityEngine.GradientAlphaKey v)
		{
			GradientAlphaKeySurrogate o =  new GradientAlphaKeySurrogate();
			o.alpha = v.alpha;
			o.time = v.time;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.GradientAlphaKey o = (UnityEngine.GradientAlphaKey)obj;
			info.AddValue("alpha", o.alpha);
			info.AddValue("time", o.time);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.GradientAlphaKey o = (UnityEngine.GradientAlphaKey)obj;
			o.alpha = (float)info.GetValue("alpha", typeof(float));
			o.time = (float)info.GetValue("time", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class GradientColorKeySurrogate : ISerializationSurrogate
#else
	public class GradientColorKeySurrogate
#endif
	{
		public UnityEngine.Color color;
		public System.Single time;
		public static implicit operator UnityEngine.GradientColorKey(GradientColorKeySurrogate v)
		{
			UnityEngine.GradientColorKey o =  new UnityEngine.GradientColorKey();
			o.color = v.color;
			o.time = v.time;
			return o;
		}
		public static implicit operator GradientColorKeySurrogate(UnityEngine.GradientColorKey v)
		{
			GradientColorKeySurrogate o =  new GradientColorKeySurrogate();
			o.color = v.color;
			o.time = v.time;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.GradientColorKey o = (UnityEngine.GradientColorKey)obj;
			info.AddValue("color", o.color);
			info.AddValue("time", o.time);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.GradientColorKey o = (UnityEngine.GradientColorKey)obj;
			o.color = (UnityEngine.Color)info.GetValue("color", typeof(UnityEngine.Color));
			o.time = (float)info.GetValue("time", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class LayerMaskSurrogate : ISerializationSurrogate
#else
	public class LayerMaskSurrogate
#endif
	{
		public System.Int32 value;
		public static implicit operator UnityEngine.LayerMask(LayerMaskSurrogate v)
		{
			UnityEngine.LayerMask o =  new UnityEngine.LayerMask();
			o.value = v.value;
			return o;
		}
		public static implicit operator LayerMaskSurrogate(UnityEngine.LayerMask v)
		{
			LayerMaskSurrogate o =  new LayerMaskSurrogate();
			o.value = v.value;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.LayerMask o = (UnityEngine.LayerMask)obj;
			info.AddValue("value", o.value);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.LayerMask o = (UnityEngine.LayerMask)obj;
			o.value = (int)info.GetValue("value", typeof(int));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class RectOffsetSurrogate : ISerializationSurrogate
#else
	public class RectOffsetSurrogate
#endif
	{
		public System.Int32 left;
		public System.Int32 right;
		public System.Int32 top;
		public System.Int32 bottom;
		public static implicit operator UnityEngine.RectOffset(RectOffsetSurrogate v)
		{
			UnityEngine.RectOffset o =  new UnityEngine.RectOffset();
			o.left = v.left;
			o.right = v.right;
			o.top = v.top;
			o.bottom = v.bottom;
			return o;
		}
		public static implicit operator RectOffsetSurrogate(UnityEngine.RectOffset v)
		{
			RectOffsetSurrogate o =  new RectOffsetSurrogate();
			o.left = v.left;
			o.right = v.right;
			o.top = v.top;
			o.bottom = v.bottom;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.RectOffset o = (UnityEngine.RectOffset)obj;
			info.AddValue("left", o.left);
			info.AddValue("right", o.right);
			info.AddValue("top", o.top);
			info.AddValue("bottom", o.bottom);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.RectOffset o = (UnityEngine.RectOffset)obj;
			o.left = (int)info.GetValue("left", typeof(int));
			o.right = (int)info.GetValue("right", typeof(int));
			o.top = (int)info.GetValue("top", typeof(int));
			o.bottom = (int)info.GetValue("bottom", typeof(int));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS.UINS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class AnimationTriggersSurrogate : ISerializationSurrogate
#else
	public class AnimationTriggersSurrogate
#endif
	{
		public System.String normalTrigger;
		public System.String highlightedTrigger;
		public System.String pressedTrigger;
		public System.String disabledTrigger;
		public static implicit operator UnityEngine.UI.AnimationTriggers(AnimationTriggersSurrogate v)
		{
			UnityEngine.UI.AnimationTriggers o =  new UnityEngine.UI.AnimationTriggers();
			o.normalTrigger = v.normalTrigger;
			o.highlightedTrigger = v.highlightedTrigger;
			o.pressedTrigger = v.pressedTrigger;
			o.disabledTrigger = v.disabledTrigger;
			return o;
		}
		public static implicit operator AnimationTriggersSurrogate(UnityEngine.UI.AnimationTriggers v)
		{
			AnimationTriggersSurrogate o =  new AnimationTriggersSurrogate();
			o.normalTrigger = v.normalTrigger;
			o.highlightedTrigger = v.highlightedTrigger;
			o.pressedTrigger = v.pressedTrigger;
			o.disabledTrigger = v.disabledTrigger;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.UI.AnimationTriggers o = (UnityEngine.UI.AnimationTriggers)obj;
			info.AddValue("normalTrigger", o.normalTrigger);
			info.AddValue("highlightedTrigger", o.highlightedTrigger);
			info.AddValue("pressedTrigger", o.pressedTrigger);
			info.AddValue("disabledTrigger", o.disabledTrigger);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.UI.AnimationTriggers o = (UnityEngine.UI.AnimationTriggers)obj;
			o.normalTrigger = (string)info.GetValue("normalTrigger", typeof(string));
			o.highlightedTrigger = (string)info.GetValue("highlightedTrigger", typeof(string));
			o.pressedTrigger = (string)info.GetValue("pressedTrigger", typeof(string));
			o.disabledTrigger = (string)info.GetValue("disabledTrigger", typeof(string));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS.UINS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class ColorBlockSurrogate : ISerializationSurrogate
#else
	public class ColorBlockSurrogate
#endif
	{
		public UnityEngine.Color normalColor;
		public UnityEngine.Color highlightedColor;
		public UnityEngine.Color pressedColor;
		public UnityEngine.Color disabledColor;
		public System.Single colorMultiplier;
		public System.Single fadeDuration;
		public static implicit operator UnityEngine.UI.ColorBlock(ColorBlockSurrogate v)
		{
			UnityEngine.UI.ColorBlock o =  new UnityEngine.UI.ColorBlock();
			o.normalColor = v.normalColor;
			o.highlightedColor = v.highlightedColor;
			o.pressedColor = v.pressedColor;
			o.disabledColor = v.disabledColor;
			o.colorMultiplier = v.colorMultiplier;
			o.fadeDuration = v.fadeDuration;
			return o;
		}
		public static implicit operator ColorBlockSurrogate(UnityEngine.UI.ColorBlock v)
		{
			ColorBlockSurrogate o =  new ColorBlockSurrogate();
			o.normalColor = v.normalColor;
			o.highlightedColor = v.highlightedColor;
			o.pressedColor = v.pressedColor;
			o.disabledColor = v.disabledColor;
			o.colorMultiplier = v.colorMultiplier;
			o.fadeDuration = v.fadeDuration;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.UI.ColorBlock o = (UnityEngine.UI.ColorBlock)obj;
			info.AddValue("normalColor", o.normalColor);
			info.AddValue("highlightedColor", o.highlightedColor);
			info.AddValue("pressedColor", o.pressedColor);
			info.AddValue("disabledColor", o.disabledColor);
			info.AddValue("colorMultiplier", o.colorMultiplier);
			info.AddValue("fadeDuration", o.fadeDuration);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.UI.ColorBlock o = (UnityEngine.UI.ColorBlock)obj;
			o.normalColor = (UnityEngine.Color)info.GetValue("normalColor", typeof(UnityEngine.Color));
			o.highlightedColor = (UnityEngine.Color)info.GetValue("highlightedColor", typeof(UnityEngine.Color));
			o.pressedColor = (UnityEngine.Color)info.GetValue("pressedColor", typeof(UnityEngine.Color));
			o.disabledColor = (UnityEngine.Color)info.GetValue("disabledColor", typeof(UnityEngine.Color));
			o.colorMultiplier = (float)info.GetValue("colorMultiplier", typeof(float));
			o.fadeDuration = (float)info.GetValue("fadeDuration", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS.AINS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class NavMeshPathSurrogate : ISerializationSurrogate
#else
	public class NavMeshPathSurrogate
#endif
	{
        public static implicit operator UnityEngine.AI.NavMeshPath(NavMeshPathSurrogate v)
        {
            UnityEngine.AI.NavMeshPath o = new UnityEngine.AI.NavMeshPath();
            return o;
        }
        public static implicit operator NavMeshPathSurrogate(UnityEngine.AI.NavMeshPath v)
        {
            NavMeshPathSurrogate o = new NavMeshPathSurrogate();
            return o;
        }

#if !UNITY_WINRT
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class ClothSkinningCoefficientSurrogate : ISerializationSurrogate
#else
	public class ClothSkinningCoefficientSurrogate
#endif
	{
		public System.Single maxDistance;
		public System.Single collisionSphereDistance;
		public static implicit operator UnityEngine.ClothSkinningCoefficient(ClothSkinningCoefficientSurrogate v)
		{
			UnityEngine.ClothSkinningCoefficient o =  new UnityEngine.ClothSkinningCoefficient();
			o.maxDistance = v.maxDistance;
			o.collisionSphereDistance = v.collisionSphereDistance;
			return o;
		}
		public static implicit operator ClothSkinningCoefficientSurrogate(UnityEngine.ClothSkinningCoefficient v)
		{
			ClothSkinningCoefficientSurrogate o =  new ClothSkinningCoefficientSurrogate();
			o.maxDistance = v.maxDistance;
			o.collisionSphereDistance = v.collisionSphereDistance;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.ClothSkinningCoefficient o = (UnityEngine.ClothSkinningCoefficient)obj;
			info.AddValue("maxDistance", o.maxDistance);
			info.AddValue("collisionSphereDistance", o.collisionSphereDistance);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.ClothSkinningCoefficient o = (UnityEngine.ClothSkinningCoefficient)obj;
			o.maxDistance = (float)info.GetValue("maxDistance", typeof(float));
			o.collisionSphereDistance = (float)info.GetValue("collisionSphereDistance", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class BoneWeightSurrogate : ISerializationSurrogate
#else
	public class BoneWeightSurrogate
#endif
	{
		public System.Single weight0;
		public System.Single weight1;
		public System.Single weight2;
		public System.Single weight3;
		public System.Int32 boneIndex0;
		public System.Int32 boneIndex1;
		public System.Int32 boneIndex2;
		public System.Int32 boneIndex3;
		public static implicit operator UnityEngine.BoneWeight(BoneWeightSurrogate v)
		{
			UnityEngine.BoneWeight o =  new UnityEngine.BoneWeight();
			o.weight0 = v.weight0;
			o.weight1 = v.weight1;
			o.weight2 = v.weight2;
			o.weight3 = v.weight3;
			o.boneIndex0 = v.boneIndex0;
			o.boneIndex1 = v.boneIndex1;
			o.boneIndex2 = v.boneIndex2;
			o.boneIndex3 = v.boneIndex3;
			return o;
		}
		public static implicit operator BoneWeightSurrogate(UnityEngine.BoneWeight v)
		{
			BoneWeightSurrogate o =  new BoneWeightSurrogate();
			o.weight0 = v.weight0;
			o.weight1 = v.weight1;
			o.weight2 = v.weight2;
			o.weight3 = v.weight3;
			o.boneIndex0 = v.boneIndex0;
			o.boneIndex1 = v.boneIndex1;
			o.boneIndex2 = v.boneIndex2;
			o.boneIndex3 = v.boneIndex3;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.BoneWeight o = (UnityEngine.BoneWeight)obj;
			info.AddValue("weight0", o.weight0);
			info.AddValue("weight1", o.weight1);
			info.AddValue("weight2", o.weight2);
			info.AddValue("weight3", o.weight3);
			info.AddValue("boneIndex0", o.boneIndex0);
			info.AddValue("boneIndex1", o.boneIndex1);
			info.AddValue("boneIndex2", o.boneIndex2);
			info.AddValue("boneIndex3", o.boneIndex3);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.BoneWeight o = (UnityEngine.BoneWeight)obj;
			o.weight0 = (float)info.GetValue("weight0", typeof(float));
			o.weight1 = (float)info.GetValue("weight1", typeof(float));
			o.weight2 = (float)info.GetValue("weight2", typeof(float));
			o.weight3 = (float)info.GetValue("weight3", typeof(float));
			o.boneIndex0 = (int)info.GetValue("boneIndex0", typeof(int));
			o.boneIndex1 = (int)info.GetValue("boneIndex1", typeof(int));
			o.boneIndex2 = (int)info.GetValue("boneIndex2", typeof(int));
			o.boneIndex3 = (int)info.GetValue("boneIndex3", typeof(int));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class TreeInstanceSurrogate : ISerializationSurrogate
#else
	public class TreeInstanceSurrogate
#endif
	{
		public UnityEngine.Vector3 position;
		public System.Single widthScale;
		public System.Single heightScale;
		public System.Single rotation;
		public UnityEngine.Color32 color;
		public UnityEngine.Color32 lightmapColor;
		public System.Int32 prototypeIndex;
		public static implicit operator UnityEngine.TreeInstance(TreeInstanceSurrogate v)
		{
			UnityEngine.TreeInstance o =  new UnityEngine.TreeInstance();
			o.position = v.position;
			o.widthScale = v.widthScale;
			o.heightScale = v.heightScale;
			o.rotation = v.rotation;
			o.color = v.color;
			o.lightmapColor = v.lightmapColor;
			o.prototypeIndex = v.prototypeIndex;
			return o;
		}
		public static implicit operator TreeInstanceSurrogate(UnityEngine.TreeInstance v)
		{
			TreeInstanceSurrogate o =  new TreeInstanceSurrogate();
			o.position = v.position;
			o.widthScale = v.widthScale;
			o.heightScale = v.heightScale;
			o.rotation = v.rotation;
			o.color = v.color;
			o.lightmapColor = v.lightmapColor;
			o.prototypeIndex = v.prototypeIndex;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.TreeInstance o = (UnityEngine.TreeInstance)obj;
			info.AddValue("position", o.position);
			info.AddValue("widthScale", o.widthScale);
			info.AddValue("heightScale", o.heightScale);
			info.AddValue("rotation", o.rotation);
			info.AddValue("color", o.color);
			info.AddValue("lightmapColor", o.lightmapColor);
			info.AddValue("prototypeIndex", o.prototypeIndex);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.TreeInstance o = (UnityEngine.TreeInstance)obj;
			o.position = (UnityEngine.Vector3)info.GetValue("position", typeof(UnityEngine.Vector3));
			o.widthScale = (float)info.GetValue("widthScale", typeof(float));
			o.heightScale = (float)info.GetValue("heightScale", typeof(float));
			o.rotation = (float)info.GetValue("rotation", typeof(float));
			o.color = (UnityEngine.Color32)info.GetValue("color", typeof(UnityEngine.Color32));
			o.lightmapColor = (UnityEngine.Color32)info.GetValue("lightmapColor", typeof(UnityEngine.Color32));
			o.prototypeIndex = (int)info.GetValue("prototypeIndex", typeof(int));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class CharacterInfoSurrogate : ISerializationSurrogate
#else
	public class CharacterInfoSurrogate
#endif
	{
		public System.Int32 index;
		public System.Int32 size;
		public uint style;
		public System.Int32 advance;
		public System.Int32 glyphWidth;
		public System.Int32 glyphHeight;
		public System.Int32 bearing;
		public System.Int32 minY;
		public System.Int32 maxY;
		public System.Int32 minX;
		public System.Int32 maxX;
		public UnityEngine.Vector2 uvBottomLeft;
		public UnityEngine.Vector2 uvBottomRight;
		public UnityEngine.Vector2 uvTopRight;
		public UnityEngine.Vector2 uvTopLeft;
		public static implicit operator UnityEngine.CharacterInfo(CharacterInfoSurrogate v)
		{
			UnityEngine.CharacterInfo o =  new UnityEngine.CharacterInfo();
			o.index = v.index;
			o.size = v.size;
			o.style = (UnityEngine.FontStyle)v.style;
			o.advance = v.advance;
			o.glyphWidth = v.glyphWidth;
			o.glyphHeight = v.glyphHeight;
			o.bearing = v.bearing;
			o.minY = v.minY;
			o.maxY = v.maxY;
			o.minX = v.minX;
			o.maxX = v.maxX;
			o.uvBottomLeft = v.uvBottomLeft;
			o.uvBottomRight = v.uvBottomRight;
			o.uvTopRight = v.uvTopRight;
			o.uvTopLeft = v.uvTopLeft;
			return o;
		}
		public static implicit operator CharacterInfoSurrogate(UnityEngine.CharacterInfo v)
		{
			CharacterInfoSurrogate o =  new CharacterInfoSurrogate();
			o.index = v.index;
			o.size = v.size;
			o.style = (uint)v.style;
			o.advance = v.advance;
			o.glyphWidth = v.glyphWidth;
			o.glyphHeight = v.glyphHeight;
			o.bearing = v.bearing;
			o.minY = v.minY;
			o.maxY = v.maxY;
			o.minX = v.minX;
			o.maxX = v.maxX;
			o.uvBottomLeft = v.uvBottomLeft;
			o.uvBottomRight = v.uvBottomRight;
			o.uvTopRight = v.uvTopRight;
			o.uvTopLeft = v.uvTopLeft;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.CharacterInfo o = (UnityEngine.CharacterInfo)obj;
			info.AddValue("index", o.index);
			info.AddValue("size", o.size);
			info.AddValue("style", o.style);
			info.AddValue("advance", o.advance);
			info.AddValue("glyphWidth", o.glyphWidth);
			info.AddValue("glyphHeight", o.glyphHeight);
			info.AddValue("bearing", o.bearing);
			info.AddValue("minY", o.minY);
			info.AddValue("maxY", o.maxY);
			info.AddValue("minX", o.minX);
			info.AddValue("maxX", o.maxX);
			info.AddValue("uvBottomLeft", o.uvBottomLeft);
			info.AddValue("uvBottomRight", o.uvBottomRight);
			info.AddValue("uvTopRight", o.uvTopRight);
			info.AddValue("uvTopLeft", o.uvTopLeft);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.CharacterInfo o = (UnityEngine.CharacterInfo)obj;
			o.index = (int)info.GetValue("index", typeof(int));
			o.size = (int)info.GetValue("size", typeof(int));
			o.style = (UnityEngine.FontStyle)info.GetValue("style", typeof(UnityEngine.FontStyle));
			o.advance = (int)info.GetValue("advance", typeof(int));
			o.glyphWidth = (int)info.GetValue("glyphWidth", typeof(int));
			o.glyphHeight = (int)info.GetValue("glyphHeight", typeof(int));
			o.bearing = (int)info.GetValue("bearing", typeof(int));
			o.minY = (int)info.GetValue("minY", typeof(int));
			o.maxY = (int)info.GetValue("maxY", typeof(int));
			o.minX = (int)info.GetValue("minX", typeof(int));
			o.maxX = (int)info.GetValue("maxX", typeof(int));
			o.uvBottomLeft = (UnityEngine.Vector2)info.GetValue("uvBottomLeft", typeof(UnityEngine.Vector2));
			o.uvBottomRight = (UnityEngine.Vector2)info.GetValue("uvBottomRight", typeof(UnityEngine.Vector2));
			o.uvTopRight = (UnityEngine.Vector2)info.GetValue("uvTopRight", typeof(UnityEngine.Vector2));
			o.uvTopLeft = (UnityEngine.Vector2)info.GetValue("uvTopLeft", typeof(UnityEngine.Vector2));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class Vector3Surrogate : ISerializationSurrogate
#else
	public class Vector3Surrogate
#endif
	{
		public System.Single x;
		public System.Single y;
		public System.Single z;
		public static implicit operator UnityEngine.Vector3(Vector3Surrogate v)
		{
			UnityEngine.Vector3 o =  new UnityEngine.Vector3();
			o.x = v.x;
			o.y = v.y;
			o.z = v.z;
			return o;
		}
		public static implicit operator Vector3Surrogate(UnityEngine.Vector3 v)
		{
			Vector3Surrogate o =  new Vector3Surrogate();
			o.x = v.x;
			o.y = v.y;
			o.z = v.z;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Vector3 o = (UnityEngine.Vector3)obj;
			info.AddValue("x", o.x);
			info.AddValue("y", o.y);
			info.AddValue("z", o.z);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Vector3 o = (UnityEngine.Vector3)obj;
			o.x = (float)info.GetValue("x", typeof(float));
			o.y = (float)info.GetValue("y", typeof(float));
			o.z = (float)info.GetValue("z", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class ColorSurrogate : ISerializationSurrogate
#else
	public class ColorSurrogate
#endif
	{
		public System.Single r;
		public System.Single g;
		public System.Single b;
		public System.Single a;
		public static implicit operator UnityEngine.Color(ColorSurrogate v)
		{
			UnityEngine.Color o =  new UnityEngine.Color();
			o.r = v.r;
			o.g = v.g;
			o.b = v.b;
			o.a = v.a;
			return o;
		}
		public static implicit operator ColorSurrogate(UnityEngine.Color v)
		{
			ColorSurrogate o =  new ColorSurrogate();
			o.r = v.r;
			o.g = v.g;
			o.b = v.b;
			o.a = v.a;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Color o = (UnityEngine.Color)obj;
			info.AddValue("r", o.r);
			info.AddValue("g", o.g);
			info.AddValue("b", o.b);
			info.AddValue("a", o.a);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Color o = (UnityEngine.Color)obj;
			o.r = (float)info.GetValue("r", typeof(float));
			o.g = (float)info.GetValue("g", typeof(float));
			o.b = (float)info.GetValue("b", typeof(float));
			o.a = (float)info.GetValue("a", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class RectSurrogate : ISerializationSurrogate
#else
	public class RectSurrogate
#endif
	{
		public System.Single x;
		public System.Single y;
		public System.Single width;
		public System.Single height;
		public static implicit operator UnityEngine.Rect(RectSurrogate v)
		{
			UnityEngine.Rect o =  new UnityEngine.Rect();
			o.x = v.x;
			o.y = v.y;
			o.width = v.width;
			o.height = v.height;
			return o;
		}
		public static implicit operator RectSurrogate(UnityEngine.Rect v)
		{
			RectSurrogate o =  new RectSurrogate();
			o.x = v.x;
			o.y = v.y;
			o.width = v.width;
			o.height = v.height;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Rect o = (UnityEngine.Rect)obj;
			info.AddValue("x", o.x);
			info.AddValue("y", o.y);
			info.AddValue("width", o.width);
			info.AddValue("height", o.height);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Rect o = (UnityEngine.Rect)obj;
			o.x = (float)info.GetValue("x", typeof(float));
			o.y = (float)info.GetValue("y", typeof(float));
			o.width = (float)info.GetValue("width", typeof(float));
			o.height = (float)info.GetValue("height", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class Matrix4x4Surrogate : ISerializationSurrogate
#else
	public class Matrix4x4Surrogate
#endif
	{
		public System.Single m00;
		public System.Single m10;
		public System.Single m20;
		public System.Single m30;
		public System.Single m01;
		public System.Single m11;
		public System.Single m21;
		public System.Single m31;
		public System.Single m02;
		public System.Single m12;
		public System.Single m22;
		public System.Single m32;
		public System.Single m03;
		public System.Single m13;
		public System.Single m23;
		public System.Single m33;
		public static implicit operator UnityEngine.Matrix4x4(Matrix4x4Surrogate v)
		{
			UnityEngine.Matrix4x4 o =  new UnityEngine.Matrix4x4();
			o.m00 = v.m00;
			o.m10 = v.m10;
			o.m20 = v.m20;
			o.m30 = v.m30;
			o.m01 = v.m01;
			o.m11 = v.m11;
			o.m21 = v.m21;
			o.m31 = v.m31;
			o.m02 = v.m02;
			o.m12 = v.m12;
			o.m22 = v.m22;
			o.m32 = v.m32;
			o.m03 = v.m03;
			o.m13 = v.m13;
			o.m23 = v.m23;
			o.m33 = v.m33;
			return o;
		}
		public static implicit operator Matrix4x4Surrogate(UnityEngine.Matrix4x4 v)
		{
			Matrix4x4Surrogate o =  new Matrix4x4Surrogate();
			o.m00 = v.m00;
			o.m10 = v.m10;
			o.m20 = v.m20;
			o.m30 = v.m30;
			o.m01 = v.m01;
			o.m11 = v.m11;
			o.m21 = v.m21;
			o.m31 = v.m31;
			o.m02 = v.m02;
			o.m12 = v.m12;
			o.m22 = v.m22;
			o.m32 = v.m32;
			o.m03 = v.m03;
			o.m13 = v.m13;
			o.m23 = v.m23;
			o.m33 = v.m33;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Matrix4x4 o = (UnityEngine.Matrix4x4)obj;
			info.AddValue("m00", o.m00);
			info.AddValue("m10", o.m10);
			info.AddValue("m20", o.m20);
			info.AddValue("m30", o.m30);
			info.AddValue("m01", o.m01);
			info.AddValue("m11", o.m11);
			info.AddValue("m21", o.m21);
			info.AddValue("m31", o.m31);
			info.AddValue("m02", o.m02);
			info.AddValue("m12", o.m12);
			info.AddValue("m22", o.m22);
			info.AddValue("m32", o.m32);
			info.AddValue("m03", o.m03);
			info.AddValue("m13", o.m13);
			info.AddValue("m23", o.m23);
			info.AddValue("m33", o.m33);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Matrix4x4 o = (UnityEngine.Matrix4x4)obj;
			o.m00 = (float)info.GetValue("m00", typeof(float));
			o.m10 = (float)info.GetValue("m10", typeof(float));
			o.m20 = (float)info.GetValue("m20", typeof(float));
			o.m30 = (float)info.GetValue("m30", typeof(float));
			o.m01 = (float)info.GetValue("m01", typeof(float));
			o.m11 = (float)info.GetValue("m11", typeof(float));
			o.m21 = (float)info.GetValue("m21", typeof(float));
			o.m31 = (float)info.GetValue("m31", typeof(float));
			o.m02 = (float)info.GetValue("m02", typeof(float));
			o.m12 = (float)info.GetValue("m12", typeof(float));
			o.m22 = (float)info.GetValue("m22", typeof(float));
			o.m32 = (float)info.GetValue("m32", typeof(float));
			o.m03 = (float)info.GetValue("m03", typeof(float));
			o.m13 = (float)info.GetValue("m13", typeof(float));
			o.m23 = (float)info.GetValue("m23", typeof(float));
			o.m33 = (float)info.GetValue("m33", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS.SceneManagementNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class SceneSurrogate : ISerializationSurrogate
#else
	public class SceneSurrogate
#endif
	{
        public static implicit operator UnityEngine.SceneManagement.Scene(SceneSurrogate v)
        {
            UnityEngine.SceneManagement.Scene o = new UnityEngine.SceneManagement.Scene();
            return o;
        }
        public static implicit operator SceneSurrogate(UnityEngine.SceneManagement.Scene v)
        {
            SceneSurrogate o = new SceneSurrogate();
            return o;
        }

#if !UNITY_WINRT
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class BoundsSurrogate : ISerializationSurrogate
#else
	public class BoundsSurrogate
#endif
	{
		public UnityEngine.Vector3 center;
		public UnityEngine.Vector3 size;
		public UnityEngine.Vector3 extents;
		public UnityEngine.Vector3 min;
		public UnityEngine.Vector3 max;
		public static implicit operator UnityEngine.Bounds(BoundsSurrogate v)
		{
			UnityEngine.Bounds o =  new UnityEngine.Bounds();
			o.center = v.center;
			o.size = v.size;
			o.extents = v.extents;
			o.min = v.min;
			o.max = v.max;
			return o;
		}
		public static implicit operator BoundsSurrogate(UnityEngine.Bounds v)
		{
			BoundsSurrogate o =  new BoundsSurrogate();
			o.center = v.center;
			o.size = v.size;
			o.extents = v.extents;
			o.min = v.min;
			o.max = v.max;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Bounds o = (UnityEngine.Bounds)obj;
			info.AddValue("center", o.center);
			info.AddValue("size", o.size);
			info.AddValue("extents", o.extents);
			info.AddValue("min", o.min);
			info.AddValue("max", o.max);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Bounds o = (UnityEngine.Bounds)obj;
			o.center = (UnityEngine.Vector3)info.GetValue("center", typeof(UnityEngine.Vector3));
			o.size = (UnityEngine.Vector3)info.GetValue("size", typeof(UnityEngine.Vector3));
			o.extents = (UnityEngine.Vector3)info.GetValue("extents", typeof(UnityEngine.Vector3));
			o.min = (UnityEngine.Vector3)info.GetValue("min", typeof(UnityEngine.Vector3));
			o.max = (UnityEngine.Vector3)info.GetValue("max", typeof(UnityEngine.Vector3));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class Vector4Surrogate : ISerializationSurrogate
#else
	public class Vector4Surrogate
#endif
	{
		public System.Single x;
		public System.Single y;
		public System.Single z;
		public System.Single w;
		public static implicit operator UnityEngine.Vector4(Vector4Surrogate v)
		{
			UnityEngine.Vector4 o =  new UnityEngine.Vector4();
			o.x = v.x;
			o.y = v.y;
			o.z = v.z;
			o.w = v.w;
			return o;
		}
		public static implicit operator Vector4Surrogate(UnityEngine.Vector4 v)
		{
			Vector4Surrogate o =  new Vector4Surrogate();
			o.x = v.x;
			o.y = v.y;
			o.z = v.z;
			o.w = v.w;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Vector4 o = (UnityEngine.Vector4)obj;
			info.AddValue("x", o.x);
			info.AddValue("y", o.y);
			info.AddValue("z", o.z);
			info.AddValue("w", o.w);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Vector4 o = (UnityEngine.Vector4)obj;
			o.x = (float)info.GetValue("x", typeof(float));
			o.y = (float)info.GetValue("y", typeof(float));
			o.z = (float)info.GetValue("z", typeof(float));
			o.w = (float)info.GetValue("w", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT && !UNITY_WEBGL
	public class NetworkViewIDSurrogate : ISerializationSurrogate
#else
    public class NetworkViewIDSurrogate
#endif
	{
#if !UNITY_WINRT && !UNITY_WEBGL
        public static implicit operator UnityEngine.NetworkViewID(NetworkViewIDSurrogate v)
        {
            UnityEngine.NetworkViewID o = new UnityEngine.NetworkViewID();
            return o;
        }
        public static implicit operator NetworkViewIDSurrogate(UnityEngine.NetworkViewID v)
        {
            NetworkViewIDSurrogate o = new NetworkViewIDSurrogate();
            return o;
        }


        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
    }
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT && !UNITY_WEBGL
	public class NetworkPlayerSurrogate : ISerializationSurrogate
#else
    public class NetworkPlayerSurrogate
#endif
	{
#if !UNITY_WINRT && !UNITY_WEBGL
        public static implicit operator UnityEngine.NetworkPlayer(NetworkPlayerSurrogate v)
        {
            UnityEngine.NetworkPlayer o = new UnityEngine.NetworkPlayer();
            return o;
        }
        public static implicit operator NetworkPlayerSurrogate(UnityEngine.NetworkPlayer v)
        {
            NetworkPlayerSurrogate o = new NetworkPlayerSurrogate();
            return o;
        }


        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
    }
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class Vector2Surrogate : ISerializationSurrogate
#else
	public class Vector2Surrogate
#endif
	{
		public System.Single x;
		public System.Single y;
		public static implicit operator UnityEngine.Vector2(Vector2Surrogate v)
		{
			UnityEngine.Vector2 o =  new UnityEngine.Vector2();
			o.x = v.x;
			o.y = v.y;
			return o;
		}
		public static implicit operator Vector2Surrogate(UnityEngine.Vector2 v)
		{
			Vector2Surrogate o =  new Vector2Surrogate();
			o.x = v.x;
			o.y = v.y;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Vector2 o = (UnityEngine.Vector2)obj;
			info.AddValue("x", o.x);
			info.AddValue("y", o.y);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Vector2 o = (UnityEngine.Vector2)obj;
			o.x = (float)info.GetValue("x", typeof(float));
			o.y = (float)info.GetValue("y", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class RenderBufferSurrogate : ISerializationSurrogate
#else
	public class RenderBufferSurrogate
#endif
	{
        public static implicit operator UnityEngine.RenderBuffer(RenderBufferSurrogate v)
        {
            UnityEngine.RenderBuffer o = new UnityEngine.RenderBuffer();
            return o;
        }
        public static implicit operator RenderBufferSurrogate(UnityEngine.RenderBuffer v)
        {
            RenderBufferSurrogate o = new RenderBufferSurrogate();
            return o;
        }

#if !UNITY_WINRT
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class QuaternionSurrogate : ISerializationSurrogate
#else
	public class QuaternionSurrogate
#endif
	{
		public System.Single x;
		public System.Single y;
		public System.Single z;
		public System.Single w;
		public UnityEngine.Vector3 eulerAngles;
		public static implicit operator UnityEngine.Quaternion(QuaternionSurrogate v)
		{
			UnityEngine.Quaternion o =  new UnityEngine.Quaternion();
			o.x = v.x;
			o.y = v.y;
			o.z = v.z;
			o.w = v.w;
			o.eulerAngles = v.eulerAngles;
			return o;
		}
		public static implicit operator QuaternionSurrogate(UnityEngine.Quaternion v)
		{
			QuaternionSurrogate o =  new QuaternionSurrogate();
			o.x = v.x;
			o.y = v.y;
			o.z = v.z;
			o.w = v.w;
			o.eulerAngles = v.eulerAngles;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Quaternion o = (UnityEngine.Quaternion)obj;
			info.AddValue("x", o.x);
			info.AddValue("y", o.y);
			info.AddValue("z", o.z);
			info.AddValue("w", o.w);
			info.AddValue("eulerAngles", o.eulerAngles);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Quaternion o = (UnityEngine.Quaternion)obj;
			o.x = (float)info.GetValue("x", typeof(float));
			o.y = (float)info.GetValue("y", typeof(float));
			o.z = (float)info.GetValue("z", typeof(float));
			o.w = (float)info.GetValue("w", typeof(float));
			o.eulerAngles = (UnityEngine.Vector3)info.GetValue("eulerAngles", typeof(UnityEngine.Vector3));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointMotorSurrogate : ISerializationSurrogate
#else
	public class JointMotorSurrogate
#endif
	{
		public System.Single targetVelocity;
		public System.Single force;
		public System.Boolean freeSpin;
		public static implicit operator UnityEngine.JointMotor(JointMotorSurrogate v)
		{
			UnityEngine.JointMotor o =  new UnityEngine.JointMotor();
			o.targetVelocity = v.targetVelocity;
			o.force = v.force;
			o.freeSpin = v.freeSpin;
			return o;
		}
		public static implicit operator JointMotorSurrogate(UnityEngine.JointMotor v)
		{
			JointMotorSurrogate o =  new JointMotorSurrogate();
			o.targetVelocity = v.targetVelocity;
			o.force = v.force;
			o.freeSpin = v.freeSpin;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointMotor o = (UnityEngine.JointMotor)obj;
			info.AddValue("targetVelocity", o.targetVelocity);
			info.AddValue("force", o.force);
			info.AddValue("freeSpin", o.freeSpin);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointMotor o = (UnityEngine.JointMotor)obj;
			o.targetVelocity = (float)info.GetValue("targetVelocity", typeof(float));
			o.force = (float)info.GetValue("force", typeof(float));
			o.freeSpin = (bool)info.GetValue("freeSpin", typeof(bool));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointLimitsSurrogate : ISerializationSurrogate
#else
	public class JointLimitsSurrogate
#endif
	{
		public System.Single min;
		public System.Single max;
		public System.Single bounciness;
		public System.Single bounceMinVelocity;
		public System.Single contactDistance;
		public static implicit operator UnityEngine.JointLimits(JointLimitsSurrogate v)
		{
			UnityEngine.JointLimits o =  new UnityEngine.JointLimits();
			o.min = v.min;
			o.max = v.max;
			o.bounciness = v.bounciness;
			o.bounceMinVelocity = v.bounceMinVelocity;
			o.contactDistance = v.contactDistance;
			return o;
		}
		public static implicit operator JointLimitsSurrogate(UnityEngine.JointLimits v)
		{
			JointLimitsSurrogate o =  new JointLimitsSurrogate();
			o.min = v.min;
			o.max = v.max;
			o.bounciness = v.bounciness;
			o.bounceMinVelocity = v.bounceMinVelocity;
			o.contactDistance = v.contactDistance;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointLimits o = (UnityEngine.JointLimits)obj;
			info.AddValue("min", o.min);
			info.AddValue("max", o.max);
			info.AddValue("bounciness", o.bounciness);
			info.AddValue("bounceMinVelocity", o.bounceMinVelocity);
			info.AddValue("contactDistance", o.contactDistance);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointLimits o = (UnityEngine.JointLimits)obj;
			o.min = (float)info.GetValue("min", typeof(float));
			o.max = (float)info.GetValue("max", typeof(float));
			o.bounciness = (float)info.GetValue("bounciness", typeof(float));
			o.bounceMinVelocity = (float)info.GetValue("bounceMinVelocity", typeof(float));
			o.contactDistance = (float)info.GetValue("contactDistance", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointSpringSurrogate : ISerializationSurrogate
#else
	public class JointSpringSurrogate
#endif
	{
		public System.Single spring;
		public System.Single damper;
		public System.Single targetPosition;
		public static implicit operator UnityEngine.JointSpring(JointSpringSurrogate v)
		{
			UnityEngine.JointSpring o =  new UnityEngine.JointSpring();
			o.spring = v.spring;
			o.damper = v.damper;
			o.targetPosition = v.targetPosition;
			return o;
		}
		public static implicit operator JointSpringSurrogate(UnityEngine.JointSpring v)
		{
			JointSpringSurrogate o =  new JointSpringSurrogate();
			o.spring = v.spring;
			o.damper = v.damper;
			o.targetPosition = v.targetPosition;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointSpring o = (UnityEngine.JointSpring)obj;
			info.AddValue("spring", o.spring);
			info.AddValue("damper", o.damper);
			info.AddValue("targetPosition", o.targetPosition);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointSpring o = (UnityEngine.JointSpring)obj;
			o.spring = (float)info.GetValue("spring", typeof(float));
			o.damper = (float)info.GetValue("damper", typeof(float));
			o.targetPosition = (float)info.GetValue("targetPosition", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointDriveSurrogate : ISerializationSurrogate
#else
	public class JointDriveSurrogate
#endif
	{
		public System.Single positionSpring;
		public System.Single positionDamper;
		public System.Single maximumForce;
		public static implicit operator UnityEngine.JointDrive(JointDriveSurrogate v)
		{
			UnityEngine.JointDrive o =  new UnityEngine.JointDrive();
			o.positionSpring = v.positionSpring;
			o.positionDamper = v.positionDamper;
			o.maximumForce = v.maximumForce;
			return o;
		}
		public static implicit operator JointDriveSurrogate(UnityEngine.JointDrive v)
		{
			JointDriveSurrogate o =  new JointDriveSurrogate();
			o.positionSpring = v.positionSpring;
			o.positionDamper = v.positionDamper;
			o.maximumForce = v.maximumForce;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointDrive o = (UnityEngine.JointDrive)obj;
			info.AddValue("positionSpring", o.positionSpring);
			info.AddValue("positionDamper", o.positionDamper);
			info.AddValue("maximumForce", o.maximumForce);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointDrive o = (UnityEngine.JointDrive)obj;
			o.positionSpring = (float)info.GetValue("positionSpring", typeof(float));
			o.positionDamper = (float)info.GetValue("positionDamper", typeof(float));
			o.maximumForce = (float)info.GetValue("maximumForce", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class SoftJointLimitSpringSurrogate : ISerializationSurrogate
#else
	public class SoftJointLimitSpringSurrogate
#endif
	{
		public System.Single spring;
		public System.Single damper;
		public static implicit operator UnityEngine.SoftJointLimitSpring(SoftJointLimitSpringSurrogate v)
		{
			UnityEngine.SoftJointLimitSpring o =  new UnityEngine.SoftJointLimitSpring();
			o.spring = v.spring;
			o.damper = v.damper;
			return o;
		}
		public static implicit operator SoftJointLimitSpringSurrogate(UnityEngine.SoftJointLimitSpring v)
		{
			SoftJointLimitSpringSurrogate o =  new SoftJointLimitSpringSurrogate();
			o.spring = v.spring;
			o.damper = v.damper;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.SoftJointLimitSpring o = (UnityEngine.SoftJointLimitSpring)obj;
			info.AddValue("spring", o.spring);
			info.AddValue("damper", o.damper);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.SoftJointLimitSpring o = (UnityEngine.SoftJointLimitSpring)obj;
			o.spring = (float)info.GetValue("spring", typeof(float));
			o.damper = (float)info.GetValue("damper", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class SoftJointLimitSurrogate : ISerializationSurrogate
#else
	public class SoftJointLimitSurrogate
#endif
	{
		public System.Single limit;
		public System.Single bounciness;
		public System.Single contactDistance;
		public static implicit operator UnityEngine.SoftJointLimit(SoftJointLimitSurrogate v)
		{
			UnityEngine.SoftJointLimit o =  new UnityEngine.SoftJointLimit();
			o.limit = v.limit;
			o.bounciness = v.bounciness;
			o.contactDistance = v.contactDistance;
			return o;
		}
		public static implicit operator SoftJointLimitSurrogate(UnityEngine.SoftJointLimit v)
		{
			SoftJointLimitSurrogate o =  new SoftJointLimitSurrogate();
			o.limit = v.limit;
			o.bounciness = v.bounciness;
			o.contactDistance = v.contactDistance;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.SoftJointLimit o = (UnityEngine.SoftJointLimit)obj;
			info.AddValue("limit", o.limit);
			info.AddValue("bounciness", o.bounciness);
			info.AddValue("contactDistance", o.contactDistance);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.SoftJointLimit o = (UnityEngine.SoftJointLimit)obj;
			o.limit = (float)info.GetValue("limit", typeof(float));
			o.bounciness = (float)info.GetValue("bounciness", typeof(float));
			o.contactDistance = (float)info.GetValue("contactDistance", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointMotor2DSurrogate : ISerializationSurrogate
#else
	public class JointMotor2DSurrogate
#endif
	{
		public System.Single motorSpeed;
		public System.Single maxMotorTorque;
		public static implicit operator UnityEngine.JointMotor2D(JointMotor2DSurrogate v)
		{
			UnityEngine.JointMotor2D o =  new UnityEngine.JointMotor2D();
			o.motorSpeed = v.motorSpeed;
			o.maxMotorTorque = v.maxMotorTorque;
			return o;
		}
		public static implicit operator JointMotor2DSurrogate(UnityEngine.JointMotor2D v)
		{
			JointMotor2DSurrogate o =  new JointMotor2DSurrogate();
			o.motorSpeed = v.motorSpeed;
			o.maxMotorTorque = v.maxMotorTorque;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointMotor2D o = (UnityEngine.JointMotor2D)obj;
			info.AddValue("motorSpeed", o.motorSpeed);
			info.AddValue("maxMotorTorque", o.maxMotorTorque);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointMotor2D o = (UnityEngine.JointMotor2D)obj;
			o.motorSpeed = (float)info.GetValue("motorSpeed", typeof(float));
			o.maxMotorTorque = (float)info.GetValue("maxMotorTorque", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointAngleLimits2DSurrogate : ISerializationSurrogate
#else
	public class JointAngleLimits2DSurrogate
#endif
	{
		public System.Single min;
		public System.Single max;
		public static implicit operator UnityEngine.JointAngleLimits2D(JointAngleLimits2DSurrogate v)
		{
			UnityEngine.JointAngleLimits2D o =  new UnityEngine.JointAngleLimits2D();
			o.min = v.min;
			o.max = v.max;
			return o;
		}
		public static implicit operator JointAngleLimits2DSurrogate(UnityEngine.JointAngleLimits2D v)
		{
			JointAngleLimits2DSurrogate o =  new JointAngleLimits2DSurrogate();
			o.min = v.min;
			o.max = v.max;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointAngleLimits2D o = (UnityEngine.JointAngleLimits2D)obj;
			info.AddValue("min", o.min);
			info.AddValue("max", o.max);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointAngleLimits2D o = (UnityEngine.JointAngleLimits2D)obj;
			o.min = (float)info.GetValue("min", typeof(float));
			o.max = (float)info.GetValue("max", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointTranslationLimits2DSurrogate : ISerializationSurrogate
#else
	public class JointTranslationLimits2DSurrogate
#endif
	{
		public System.Single min;
		public System.Single max;
		public static implicit operator UnityEngine.JointTranslationLimits2D(JointTranslationLimits2DSurrogate v)
		{
			UnityEngine.JointTranslationLimits2D o =  new UnityEngine.JointTranslationLimits2D();
			o.min = v.min;
			o.max = v.max;
			return o;
		}
		public static implicit operator JointTranslationLimits2DSurrogate(UnityEngine.JointTranslationLimits2D v)
		{
			JointTranslationLimits2DSurrogate o =  new JointTranslationLimits2DSurrogate();
			o.min = v.min;
			o.max = v.max;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointTranslationLimits2D o = (UnityEngine.JointTranslationLimits2D)obj;
			info.AddValue("min", o.min);
			info.AddValue("max", o.max);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointTranslationLimits2D o = (UnityEngine.JointTranslationLimits2D)obj;
			o.min = (float)info.GetValue("min", typeof(float));
			o.max = (float)info.GetValue("max", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class JointSuspension2DSurrogate : ISerializationSurrogate
#else
	public class JointSuspension2DSurrogate
#endif
	{
		public System.Single dampingRatio;
		public System.Single frequency;
		public System.Single angle;
		public static implicit operator UnityEngine.JointSuspension2D(JointSuspension2DSurrogate v)
		{
			UnityEngine.JointSuspension2D o =  new UnityEngine.JointSuspension2D();
			o.dampingRatio = v.dampingRatio;
			o.frequency = v.frequency;
			o.angle = v.angle;
			return o;
		}
		public static implicit operator JointSuspension2DSurrogate(UnityEngine.JointSuspension2D v)
		{
			JointSuspension2DSurrogate o =  new JointSuspension2DSurrogate();
			o.dampingRatio = v.dampingRatio;
			o.frequency = v.frequency;
			o.angle = v.angle;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.JointSuspension2D o = (UnityEngine.JointSuspension2D)obj;
			info.AddValue("dampingRatio", o.dampingRatio);
			info.AddValue("frequency", o.frequency);
			info.AddValue("angle", o.angle);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.JointSuspension2D o = (UnityEngine.JointSuspension2D)obj;
			o.dampingRatio = (float)info.GetValue("dampingRatio", typeof(float));
			o.frequency = (float)info.GetValue("frequency", typeof(float));
			o.angle = (float)info.GetValue("angle", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class WheelFrictionCurveSurrogate : ISerializationSurrogate
#else
	public class WheelFrictionCurveSurrogate
#endif
	{
		public System.Single extremumSlip;
		public System.Single extremumValue;
		public System.Single asymptoteSlip;
		public System.Single asymptoteValue;
		public System.Single stiffness;
		public static implicit operator UnityEngine.WheelFrictionCurve(WheelFrictionCurveSurrogate v)
		{
			UnityEngine.WheelFrictionCurve o =  new UnityEngine.WheelFrictionCurve();
			o.extremumSlip = v.extremumSlip;
			o.extremumValue = v.extremumValue;
			o.asymptoteSlip = v.asymptoteSlip;
			o.asymptoteValue = v.asymptoteValue;
			o.stiffness = v.stiffness;
			return o;
		}
		public static implicit operator WheelFrictionCurveSurrogate(UnityEngine.WheelFrictionCurve v)
		{
			WheelFrictionCurveSurrogate o =  new WheelFrictionCurveSurrogate();
			o.extremumSlip = v.extremumSlip;
			o.extremumValue = v.extremumValue;
			o.asymptoteSlip = v.asymptoteSlip;
			o.asymptoteValue = v.asymptoteValue;
			o.stiffness = v.stiffness;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.WheelFrictionCurve o = (UnityEngine.WheelFrictionCurve)obj;
			info.AddValue("extremumSlip", o.extremumSlip);
			info.AddValue("extremumValue", o.extremumValue);
			info.AddValue("asymptoteSlip", o.asymptoteSlip);
			info.AddValue("asymptoteValue", o.asymptoteValue);
			info.AddValue("stiffness", o.stiffness);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.WheelFrictionCurve o = (UnityEngine.WheelFrictionCurve)obj;
			o.extremumSlip = (float)info.GetValue("extremumSlip", typeof(float));
			o.extremumValue = (float)info.GetValue("extremumValue", typeof(float));
			o.asymptoteSlip = (float)info.GetValue("asymptoteSlip", typeof(float));
			o.asymptoteValue = (float)info.GetValue("asymptoteValue", typeof(float));
			o.stiffness = (float)info.GetValue("stiffness", typeof(float));
			return o;
		}
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS.AINS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class OffMeshLinkDataSurrogate : ISerializationSurrogate
#else
	public class OffMeshLinkDataSurrogate
#endif
	{
        public static implicit operator UnityEngine.AI.OffMeshLinkData(OffMeshLinkDataSurrogate v)
        {
            return new UnityEngine.AI.OffMeshLinkData();
        }
        public static implicit operator OffMeshLinkDataSurrogate(UnityEngine.AI.OffMeshLinkData v)
        {
            return new OffMeshLinkDataSurrogate();
        }
#if !UNITY_WINRT
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS.ExperimentalNS.DirectorNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class PlayableGraphSurrogate : ISerializationSurrogate
#else
	public class PlayableGraphSurrogate
#endif
	{
        public static implicit operator PlayableGraph(PlayableGraphSurrogate v)
        {
            return new PlayableGraph();
        }
        public static implicit operator PlayableGraphSurrogate(PlayableGraph v)
        {
            return new PlayableGraphSurrogate();
        }

#if !UNITY_WINRT
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) { }
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) { return obj; }
#endif
	}
}

namespace Battlehub.RTSaveLoad.UnityEngineNS
{
#if RT_USE_PROTOBUF
	[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
#if !UNITY_WINRT
	public class Color32Surrogate : ISerializationSurrogate
#else
	public class Color32Surrogate
#endif
	{
		public System.Byte r;
		public System.Byte g;
		public System.Byte b;
		public System.Byte a;
		public static implicit operator UnityEngine.Color32(Color32Surrogate v)
		{
			UnityEngine.Color32 o =  new UnityEngine.Color32();
			o.r = v.r;
			o.g = v.g;
			o.b = v.b;
			o.a = v.a;
			return o;
		}
		public static implicit operator Color32Surrogate(UnityEngine.Color32 v)
		{
			Color32Surrogate o =  new Color32Surrogate();
			o.r = v.r;
			o.g = v.g;
			o.b = v.b;
			o.a = v.a;
			return o;
		}
#if !UNITY_WINRT
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			UnityEngine.Color32 o = (UnityEngine.Color32)obj;
			info.AddValue("r", o.r);
			info.AddValue("g", o.g);
			info.AddValue("b", o.b);
			info.AddValue("a", o.a);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			UnityEngine.Color32 o = (UnityEngine.Color32)obj;
			o.r = (byte)info.GetValue("r", typeof(byte));
			o.g = (byte)info.GetValue("g", typeof(byte));
			o.b = (byte)info.GetValue("b", typeof(byte));
			o.a = (byte)info.GetValue("a", typeof(byte));
			return o;
		}
#endif
	}
}

