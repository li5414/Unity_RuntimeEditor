using ProtoBuf.Meta;
using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;
using System;
using System.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad2
{
   public static partial class TypeModelCreator
   {
       static partial void RegisterAutoTypes(RuntimeTypeModel model)
       {
           model.Add(typeof(PersistentObject), false);
                
            model.Add(typeof(PersistentMeshFilter), false);
                
            model.Add(typeof(PersistentObject), false).SetSurrogate(typeof(PersistentObject));
;
                
            
       }
   }
}

