using ProtoBuf.Meta;
using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad2
{
   public static partial class TypeModelCreator
   {
       static partial void RegisterAutoTypes(RuntimeTypeModel model)
       {
            model.Add(typeof(PersistentMeshRenderer), true);
            model.Add(typeof(PersistentSkinnedMeshRenderer), true);
            model.Add(typeof(PersistentRenderer), true);
            
       }
   }
}

