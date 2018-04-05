using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Timeline;
using UnityEngine.Timeline.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Timeline.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentTimelineAssetNestedEditorSettings : PersistentSurrogate
    {
        public static implicit operator TimelineAsset.EditorSettings(PersistentTimelineAssetNestedEditorSettings surrogate)
        {
            return (TimelineAsset.EditorSettings)surrogate.WriteTo(new TimelineAsset.EditorSettings());
        }
        
        public static implicit operator PersistentTimelineAssetNestedEditorSettings(TimelineAsset.EditorSettings obj)
        {
            PersistentTimelineAssetNestedEditorSettings surrogate = new PersistentTimelineAssetNestedEditorSettings();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}

