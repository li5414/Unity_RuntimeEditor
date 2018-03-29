
using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.Serialization;

namespace Battlehub.RTSaveLoad
{
    

    public class TypeModelCreator 
    {
        public RuntimeTypeModel Create()
        {
            RuntimeTypeModel model = TypeModel.Create();

            RegisterTypes(model);

            return model;
        }

        protected void RegisterTypes(RuntimeTypeModel model)
        {
            Type[] serializableTypes = Reflection.GetAllFromCurrentAssembly()
              .Where(type => type.IsDefined(typeof(ProtoContractAttribute), false)).ToArray();

            foreach (Type type in serializableTypes)
            {
                if (type.IsGenericType())
                {
                    continue;
                }

                model.Add(type, true);
                model.Add(typeof(PrimitiveContract<>).MakeGenericType(type.MakeArrayType()), true);
                model.Add(typeof(List<>).MakeGenericType(type), true);
            }

            Type[] primitiveTypes = new[] {
                typeof(bool),
                typeof(char),
                typeof(byte),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(string),
                typeof(float),
                typeof(double),
                typeof(decimal) };


            foreach (Type type in primitiveTypes)
            {
                if (type.IsGenericType())
                {
                    continue;
                }
                model.Add(typeof(List<>).MakeGenericType(type), true);
            }

#if !UNITY_WINRT
            Dictionary<System.Type, ISerializationSurrogate> surrogates = SerializationSurrogates.GetSurrogates();
            foreach (KeyValuePair<System.Type, ISerializationSurrogate> surrogate in surrogates)
#else
                        Dictionary<System.Type, object> surrogates = SerializationSurrogates.GetSurrogates();
                        foreach (KeyValuePair<System.Type, object> surrogate in surrogates)
#endif
            {

                model.Add(surrogate.Value.GetType(), true);
                model.Add(surrogate.Key, false).SetSurrogate(surrogate.Value.GetType());

                model.Add(typeof(PrimitiveContract<>).MakeGenericType(surrogate.Key.MakeArrayType()), true);
                model.Add(typeof(List<>).MakeGenericType(surrogate.Key), true);
            }
        }
    }
}

