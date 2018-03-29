#define RT_USE_PROTOBUF

using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ProtoBuf;

#if !UNITY_WINRT
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Battlehub.RTSaveLoad
{
    public static partial class SerializationSurrogates
    {
        private static Dictionary<Type, ISerializationSurrogate> m_surrogates = new Dictionary<Type, ISerializationSurrogate>();

        public static SurrogateSelector CreateSelector()
        {
            SurrogateSelector selector = new SurrogateSelector();
            foreach (KeyValuePair<Type, ISerializationSurrogate> kvp in m_surrogates)
            {
                selector.AddSurrogate(kvp.Key, new StreamingContext(StreamingContextStates.All), kvp.Value);
            }
            return selector;
        }

        public static Dictionary<Type, ISerializationSurrogate> GetSurrogates()
        {
            return m_surrogates;
        }
    }

    public class Binder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
        }
    }

    public static class BinarySerializer
    {
        public static TData Deserialize<TData>(byte[] b)
        {
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                // formatter.Binder = new Binder();
                formatter.SurrogateSelector = SerializationSurrogates.CreateSelector();
                stream.Seek(0, SeekOrigin.Begin);
                object deserialized = formatter.Deserialize(stream);
                return (TData)deserialized;
            }
        }

        public static TData DeserializeFromString<TData>(string data)
        {
            return Deserialize<TData>(Convert.FromBase64String(data));
        }

        public static byte[] Serialize<TData>(TData settings)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                // formatter.Binder = new Binder();
                formatter.SurrogateSelector = SerializationSurrogates.CreateSelector();
                formatter.Serialize(stream, settings);
                stream.Flush();
                stream.Position = 0;
                return stream.ToArray();
            }
        }

        public static string SerializeToString<TData>(TData settings)
        {
            return Convert.ToBase64String(Serialize(settings));
        }
    }

}
#else
namespace Battlehub.RTSaveLoad
{
    public static partial class SerializationSurrogates
    {
        private static Dictionary<Type, object> m_surrogates = new Dictionary<Type, object>();

        public static Dictionary<Type, object> GetSurrogates()
        {
            return m_surrogates;
        }
    }
}
#endif

namespace Battlehub.RTSaveLoad
{
    #if RT_USE_PROTOBUF
    [ProtoBuf.ProtoContract]
    public class NilContainer { }

    public static class ProtobufSerializer
    {
#if !UNITY_EDITOR && !UNITY_WSA
        private static RTTypeModel model = new RTTypeModel();
#else
        private static ProtoBuf.Meta.RuntimeTypeModel model = new TypeModelCreator().Create();
#endif
        static ProtobufSerializer()
        {
            model.DynamicTypeFormatting += (sender, args) =>
            {
                if (args.FormattedName == null)
                {
                    return;
                }

                if (Type.GetType(args.FormattedName) == null)
                {
                    args.Type = typeof(NilContainer);
                }
            };

#if UNITY_EDITOR 
            model.CompileInPlace();
#endif
        }

        public static TData DeepClone<TData>(TData data)
        {
            return (TData)model.DeepClone(data);
        }

        public static TData Deserialize<TData>(byte[] b)
        {
            using (var stream = new MemoryStream(b))
            {
                TData deserialized = (TData)model.Deserialize(stream, null, typeof(TData));
                return deserialized;
            }
        }

        public static byte[] Serialize<TData>(TData data)
        {
            using (var stream = new MemoryStream())
            {
                model.Serialize(stream, data);
                stream.Flush();
                stream.Position = 0;
                return stream.ToArray();
            }
        }

     
    }
#endif

        public class Serializer : ISerializer
    {
        public byte[] Serialize<TData>(TData data)
        {
            #if RT_USE_PROTOBUF
            return ProtobufSerializer.Serialize(data);
            #else
            return BinarySerializer.Serialize(data);
            #endif
        }

        public TData Deserialize<TData>(byte[] data)
        {
            #if RT_USE_PROTOBUF
            return ProtobufSerializer.Deserialize<TData>(data);
            #else
            return BinarySerializer.Deserialize<TData>(data);
            #endif
        }

        public TData DeepClone<TData>(TData data)
        {
            #if RT_USE_PROTOBUF
            return ProtobufSerializer.DeepClone(data);
            #else
            return BinarySerializer.Deserialize<TData>(BinarySerializer.Serialize(data));
            #endif
        }
    }
}

