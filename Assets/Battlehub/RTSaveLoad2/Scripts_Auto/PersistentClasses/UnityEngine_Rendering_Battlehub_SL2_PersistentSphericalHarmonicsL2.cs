using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Rendering.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSphericalHarmonicsL2 : PersistentSurrogate
    {
        public static implicit operator SphericalHarmonicsL2(PersistentSphericalHarmonicsL2 surrogate)
        {
            return (SphericalHarmonicsL2)surrogate.WriteTo(new SphericalHarmonicsL2());
        }
        
        public static implicit operator PersistentSphericalHarmonicsL2(SphericalHarmonicsL2 obj)
        {
            PersistentSphericalHarmonicsL2 surrogate = new PersistentSphericalHarmonicsL2();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}

