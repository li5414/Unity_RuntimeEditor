using ProtoBuf.Meta;
using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Match.Battlehub.SL2;
using UnityEngine.AI;
using UnityEngine.AI.Battlehub.SL2;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Types.Battlehub.SL2;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.NetworkSystem.Battlehub.SL2;
using UnityEngine.Timeline;
using UnityEngine.Timeline.Battlehub.SL2;
using UnityEngine.Playables;
using UnityEngine.Playables.Battlehub.SL2;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement.Battlehub.SL2;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad2
{
   public static partial class TypeModelCreator
   {
       static partial void RegisterAutoTypes(RuntimeTypeModel model)
       {
           model.Add(typeof(PersistentObject), false);
                
            model.Add(typeof(PersistentVector2), false).SetSurrogate(typeof(PersistentVector2));
;
                
            model.Add(typeof(PersistentVector3), false).SetSurrogate(typeof(PersistentVector3));
;
                
            model.Add(typeof(PersistentVector4), false).SetSurrogate(typeof(PersistentVector4));
;
                
            model.Add(typeof(PersistentVector2Int), false).SetSurrogate(typeof(PersistentVector2Int));
;
                
            model.Add(typeof(PersistentVector3Int), false).SetSurrogate(typeof(PersistentVector3Int));
;
                
            model.Add(typeof(PersistentColor), false).SetSurrogate(typeof(PersistentColor));
;
                
            model.Add(typeof(PersistentColor32), false).SetSurrogate(typeof(PersistentColor32));
;
                
            model.Add(typeof(PersistentMatrix4x4), false).SetSurrogate(typeof(PersistentMatrix4x4));
;
                
            model.Add(typeof(PersistentAnimationClipPair), false).SetSurrogate(typeof(PersistentAnimationClipPair));
;
                
            model.Add(typeof(PersistentAnimationCurve), false).SetSurrogate(typeof(PersistentAnimationCurve));
;
                
            model.Add(typeof(PersistentAnimationEvent), false).SetSurrogate(typeof(PersistentAnimationEvent));
;
                
            model.Add(typeof(PersistentAnimationState), false).SetSurrogate(typeof(PersistentAnimationState));
;
                
            model.Add(typeof(PersistentAnimationTriggers), false).SetSurrogate(typeof(PersistentAnimationTriggers));
;
                
            model.Add(typeof(PersistentAnimatorControllerParameter), false).SetSurrogate(typeof(PersistentAnimatorControllerParameter));
;
                
            model.Add(typeof(PersistentButtonNestedButtonClickedEvent), false).SetSurrogate(typeof(PersistentButtonNestedButtonClickedEvent));
;
                
            model.Add(typeof(PersistentConnectionConfig), false).SetSurrogate(typeof(PersistentConnectionConfig));
;
                
            model.Add(typeof(PersistentDetailPrototype), false).SetSurrogate(typeof(PersistentDetailPrototype));
;
                
            model.Add(typeof(PersistentDropdownNestedDropdownEvent), false).SetSurrogate(typeof(PersistentDropdownNestedDropdownEvent));
;
                
            model.Add(typeof(PersistentGlobalConfig), false).SetSurrogate(typeof(PersistentGlobalConfig));
;
                
            model.Add(typeof(PersistentGradient), false).SetSurrogate(typeof(PersistentGradient));
;
                
            model.Add(typeof(PersistentGUISettings), false).SetSurrogate(typeof(PersistentGUISettings));
;
                
            model.Add(typeof(PersistentGUIStyle), false).SetSurrogate(typeof(PersistentGUIStyle));
;
                
            model.Add(typeof(PersistentGUIStyleState), false).SetSurrogate(typeof(PersistentGUIStyleState));
;
                
            model.Add(typeof(PersistentInputFieldNestedOnChangeEvent), false).SetSurrogate(typeof(PersistentInputFieldNestedOnChangeEvent));
;
                
            model.Add(typeof(PersistentInputFieldNestedSubmitEvent), false).SetSurrogate(typeof(PersistentInputFieldNestedSubmitEvent));
;
                
            model.Add(typeof(PersistentMaskableGraphicNestedCullStateChangedEvent), false).SetSurrogate(typeof(PersistentMaskableGraphicNestedCullStateChangedEvent));
;
                
            model.Add(typeof(PersistentMatchInfo), false).SetSurrogate(typeof(PersistentMatchInfo));
;
                
            model.Add(typeof(PersistentNavMeshPath), false).SetSurrogate(typeof(PersistentNavMeshPath));
;
                
            model.Add(typeof(PersistentNetworkAccessToken), false).SetSurrogate(typeof(PersistentNetworkAccessToken));
;
                
            model.Add(typeof(PersistentNetworkClient), false).SetSurrogate(typeof(PersistentNetworkClient));
;
                
            model.Add(typeof(PersistentNetworkConnection), false).SetSurrogate(typeof(PersistentNetworkConnection));
;
                
            model.Add(typeof(PersistentPeerInfoMessage), false).SetSurrogate(typeof(PersistentPeerInfoMessage));
;
                
            model.Add(typeof(PersistentRectOffset), false).SetSurrogate(typeof(PersistentRectOffset));
;
                
            model.Add(typeof(PersistentScrollbarNestedScrollEvent), false).SetSurrogate(typeof(PersistentScrollbarNestedScrollEvent));
;
                
            model.Add(typeof(PersistentScrollRectNestedScrollRectEvent), false).SetSurrogate(typeof(PersistentScrollRectNestedScrollRectEvent));
;
                
            model.Add(typeof(PersistentSliderNestedSliderEvent), false).SetSurrogate(typeof(PersistentSliderNestedSliderEvent));
;
                
            model.Add(typeof(PersistentSplatPrototype), false).SetSurrogate(typeof(PersistentSplatPrototype));
;
                
            model.Add(typeof(PersistentTextGenerator), false).SetSurrogate(typeof(PersistentTextGenerator));
;
                
            model.Add(typeof(PersistentTimelineAssetNestedEditorSettings), false).SetSurrogate(typeof(PersistentTimelineAssetNestedEditorSettings));
;
                
            model.Add(typeof(PersistentToggleNestedToggleEvent), false).SetSurrogate(typeof(PersistentToggleNestedToggleEvent));
;
                
            model.Add(typeof(PersistentTreePrototype), false).SetSurrogate(typeof(PersistentTreePrototype));
;
                
            model.Add(typeof(PersistentAnimatorClipInfo), false).SetSurrogate(typeof(PersistentAnimatorClipInfo));
;
                
            model.Add(typeof(PersistentAnimatorStateInfo), false).SetSurrogate(typeof(PersistentAnimatorStateInfo));
;
                
            model.Add(typeof(PersistentBoneWeight), false).SetSurrogate(typeof(PersistentBoneWeight));
;
                
            model.Add(typeof(PersistentBounds), false).SetSurrogate(typeof(PersistentBounds));
;
                
            model.Add(typeof(PersistentBoundsInt), false).SetSurrogate(typeof(PersistentBoundsInt));
;
                
            model.Add(typeof(PersistentBoundsIntNestedPositionEnumerator), false).SetSurrogate(typeof(PersistentBoundsIntNestedPositionEnumerator));
;
                
            model.Add(typeof(PersistentCharacterInfo), false).SetSurrogate(typeof(PersistentCharacterInfo));
;
                
            model.Add(typeof(PersistentClothSkinningCoefficient), false).SetSurrogate(typeof(PersistentClothSkinningCoefficient));
;
                
            model.Add(typeof(PersistentClothSphereColliderPair), false).SetSurrogate(typeof(PersistentClothSphereColliderPair));
;
                
            model.Add(typeof(PersistentColorBlock), false).SetSurrogate(typeof(PersistentColorBlock));
;
                
            model.Add(typeof(PersistentFrustumPlanes), false).SetSurrogate(typeof(PersistentFrustumPlanes));
;
                
            model.Add(typeof(PersistentGradientAlphaKey), false).SetSurrogate(typeof(PersistentGradientAlphaKey));
;
                
            model.Add(typeof(PersistentGradientColorKey), false).SetSurrogate(typeof(PersistentGradientColorKey));
;
                
            model.Add(typeof(PersistentHash128), false).SetSurrogate(typeof(PersistentHash128));
;
                
            model.Add(typeof(PersistentJointAngleLimits2D), false).SetSurrogate(typeof(PersistentJointAngleLimits2D));
;
                
            model.Add(typeof(PersistentJointDrive), false).SetSurrogate(typeof(PersistentJointDrive));
;
                
            model.Add(typeof(PersistentJointLimits), false).SetSurrogate(typeof(PersistentJointLimits));
;
                
            model.Add(typeof(PersistentJointMotor), false).SetSurrogate(typeof(PersistentJointMotor));
;
                
            model.Add(typeof(PersistentJointMotor2D), false).SetSurrogate(typeof(PersistentJointMotor2D));
;
                
            model.Add(typeof(PersistentJointSpring), false).SetSurrogate(typeof(PersistentJointSpring));
;
                
            model.Add(typeof(PersistentJointSuspension2D), false).SetSurrogate(typeof(PersistentJointSuspension2D));
;
                
            model.Add(typeof(PersistentJointTranslationLimits2D), false).SetSurrogate(typeof(PersistentJointTranslationLimits2D));
;
                
            model.Add(typeof(PersistentKeyframe), false).SetSurrogate(typeof(PersistentKeyframe));
;
                
            model.Add(typeof(PersistentLayerMask), false).SetSurrogate(typeof(PersistentLayerMask));
;
                
            model.Add(typeof(PersistentLightBakingOutput), false).SetSurrogate(typeof(PersistentLightBakingOutput));
;
                
            model.Add(typeof(PersistentNavigation), false).SetSurrogate(typeof(PersistentNavigation));
;
                
            model.Add(typeof(PersistentNetworkHash128), false).SetSurrogate(typeof(PersistentNetworkHash128));
;
                
            model.Add(typeof(PersistentNetworkInstanceId), false).SetSurrogate(typeof(PersistentNetworkInstanceId));
;
                
            model.Add(typeof(PersistentNetworkPlayer), false).SetSurrogate(typeof(PersistentNetworkPlayer));
;
                
            model.Add(typeof(PersistentNetworkSceneId), false).SetSurrogate(typeof(PersistentNetworkSceneId));
;
                
            model.Add(typeof(PersistentNetworkViewID), false).SetSurrogate(typeof(PersistentNetworkViewID));
;
                
            model.Add(typeof(PersistentOffMeshLinkData), false).SetSurrogate(typeof(PersistentOffMeshLinkData));
;
                
            model.Add(typeof(PersistentParticle), false).SetSurrogate(typeof(PersistentParticle));
;
                
            model.Add(typeof(PersistentParticleSystemNestedCollisionModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedCollisionModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedColorBySpeedModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedColorBySpeedModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedColorOverLifetimeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedColorOverLifetimeModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedCustomDataModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedCustomDataModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedEmissionModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedEmissionModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedExternalForcesModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedExternalForcesModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedForceOverLifetimeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedForceOverLifetimeModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedInheritVelocityModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedInheritVelocityModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedLightsModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedLightsModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedLimitVelocityOverLifetimeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedLimitVelocityOverLifetimeModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedMainModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedMainModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedMinMaxCurve), false).SetSurrogate(typeof(PersistentParticleSystemNestedMinMaxCurve));
;
                
            model.Add(typeof(PersistentParticleSystemNestedMinMaxGradient), false).SetSurrogate(typeof(PersistentParticleSystemNestedMinMaxGradient));
;
                
            model.Add(typeof(PersistentParticleSystemNestedNoiseModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedNoiseModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedRotationBySpeedModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedRotationBySpeedModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedRotationOverLifetimeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedRotationOverLifetimeModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedShapeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedShapeModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedSizeBySpeedModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedSizeBySpeedModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedSizeOverLifetimeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedSizeOverLifetimeModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedSubEmittersModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedSubEmittersModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedTextureSheetAnimationModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedTextureSheetAnimationModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedTrailModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedTrailModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedTriggerModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedTriggerModule));
;
                
            model.Add(typeof(PersistentParticleSystemNestedVelocityOverLifetimeModule), false).SetSurrogate(typeof(PersistentParticleSystemNestedVelocityOverLifetimeModule));
;
                
            model.Add(typeof(PersistentPeerInfoPlayer), false).SetSurrogate(typeof(PersistentPeerInfoPlayer));
;
                
            model.Add(typeof(PersistentPlayableGraph), false).SetSurrogate(typeof(PersistentPlayableGraph));
;
                
            model.Add(typeof(PersistentPose), false).SetSurrogate(typeof(PersistentPose));
;
                
            model.Add(typeof(PersistentQuaternion), false).SetSurrogate(typeof(PersistentQuaternion));
;
                
            model.Add(typeof(PersistentRect), false).SetSurrogate(typeof(PersistentRect));
;
                
            model.Add(typeof(PersistentRenderBuffer), false).SetSurrogate(typeof(PersistentRenderBuffer));
;
                
            model.Add(typeof(PersistentRenderTextureDescriptor), false).SetSurrogate(typeof(PersistentRenderTextureDescriptor));
;
                
            model.Add(typeof(PersistentScene), false).SetSurrogate(typeof(PersistentScene));
;
                
            model.Add(typeof(PersistentSoftJointLimit), false).SetSurrogate(typeof(PersistentSoftJointLimit));
;
                
            model.Add(typeof(PersistentSoftJointLimitSpring), false).SetSurrogate(typeof(PersistentSoftJointLimitSpring));
;
                
            model.Add(typeof(PersistentSphericalHarmonicsL2), false).SetSurrogate(typeof(PersistentSphericalHarmonicsL2));
;
                
            model.Add(typeof(PersistentSpriteState), false).SetSurrogate(typeof(PersistentSpriteState));
;
                
            model.Add(typeof(PersistentTreeInstance), false).SetSurrogate(typeof(PersistentTreeInstance));
;
                
            model.Add(typeof(PersistentWheelFrictionCurve), false).SetSurrogate(typeof(PersistentWheelFrictionCurve));
;
                
            
       }
   }
}

