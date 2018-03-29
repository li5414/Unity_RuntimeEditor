using UnityEngine;
using System.Collections;
using Battlehub.Utils;

namespace Battlehub.RTSaveLoad
{
    [System.Serializable]
    public struct ObjectToID
    {
        [HideInInspector]
        public string Name;
        public Object Object;
        [ReadOnly]
        public int Id;

        public ObjectToID(Object obj, int id)
        {
            Name = obj.name;
            Object = obj;
            Id = id;
        }
    }

    public class ResourceGroup : MonoBehaviour
    {
        [ReadOnly]
        public string Guid;
        public ObjectToID[] Mapping;

        
    }
}
