using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using ProtoBuf;
using System;
using ProtoBuf.Meta;
using UnityEngine;
using System.IO;
using Battlehub.RTSaveLoad;

namespace Battlehub.RTSaveLoad2
{
    public

    /*Serialization of object field*/
    [ProtoContract]
    public class TestData1
    {
        [ProtoMember(1, DynamicType = true)]
        public object[] F;

        [ProtoMember(2)]
        public int V;
    }

    [ProtoContract]
    public class TestData2 : TestData1
    {
        [ProtoMember(1)]
        public string Str;
    }

    [ProtoContract]
    public class TestData3
    {
        [ProtoMember(2)]
        public Vector3[] V;
    }

    public abstract class Primitive
    {
        public static Primitive<T> Create<T>(T value)
        {
            return new Primitive<T>(value);
        }

        public static Primitive Create(Type type)
        {
            Type d1 = typeof(Primitive<>);
            Type constructed = d1.MakeGenericType(type);
            return (Primitive)Activator.CreateInstance(constructed);
        }

        public object ValueBase
        {
            get { return ValueImpl; }
            set { ValueImpl = value; }
        }
        protected abstract object ValueImpl { get; set; }
        protected Primitive() { }
    }

    [ProtoContract]
    public class Primitive<T> : Primitive
    {
        public Primitive() { }
        public Primitive(T value) { Value = value; }
        [ProtoMember(1)]
        public T Value { get; set; }
        protected override object ValueImpl
        {
            get { return Value; }
            set { Value = (T)value; }
        }
    }

    


  

    public class TypeModelCreator2
    {
        public RuntimeTypeModel Create()
        {
            RuntimeTypeModel model = TypeModel.Create();
            RegisterTypes(model);
            return model;
        }

        protected void RegisterTypes(RuntimeTypeModel model)
        {
            model.Add(typeof(Vector3), false).SetSurrogate(typeof(Vector3Surrogate));
            //model.Add(typeof(TestData1), true).AddSubType(500, typeof(TestData2))
        }
    }


    public static class ProtobufSerializer2
    {
        private static ProtoBuf.Meta.RuntimeTypeModel model = new TypeModelCreator2().Create();

        static ProtobufSerializer2()
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


    [ProtoContract()]
    public class PackedByteArray
    {
        [ProtoMember(1, IsPacked = true)]
        private byte[] bytes;

        public PackedByteArray()
        {
            bytes = new byte[1000 * 1024 * 100];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = (byte)(i % 255);
            }
        }
    }

    [ProtoContract()]
    public class ByteArray
    {
        [ProtoMember(1)]
        private byte[] bytes;

        public ByteArray()
        {
            bytes = new byte[1000 * 1024 * 100];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = (byte)(i % 255);
            }
        }
    }
}


namespace Battlehub.RTSaveLoad2.Tests
{


    public class Test1
    {

        [Test]
        public void TestPass1()
        {
            Assert.DoesNotThrow(() =>
            {
                TestData1 data1 = new TestData1();
                data1.F = new object[]
                {
                    "Sadasdasd",
                    new TestData2 { Str = "Str",  }
                };
                
                ProtobufSerializer2.Serialize(data1);
            });
        }

        [Test]
        public void TestPass2()
        {
            TestData2 data2 = new TestData2();
            data2.Str = "Str2";
            data2.V = 2;

            byte[] b = ProtobufSerializer2.Serialize(data2);
            TestData2 recovered2 = ProtobufSerializer2.Deserialize<TestData2>(b);

            Assert.AreEqual(data2.Str, recovered2.Str);
            Assert.AreEqual(data2.V, recovered2.V);
            
        }


        [Test]
        public void TestPass3()
        {
            Assert.DoesNotThrow(() =>
            {
                TestData3 data3 = new TestData3();
                ProtobufSerializer2.Serialize(data3);
            });
        }

        [Test]
        public void PackedSerialize()
        {
            Assert.DoesNotThrow(() =>
            {
                PackedByteArray p = new PackedByteArray();
                ProtobufSerializer2.Serialize(p);
            });
        }

        [Test]
        public void PackedDeserialize()
        {
            Assert.DoesNotThrow(() =>
            {
                PackedByteArray p = new PackedByteArray();
                ProtobufSerializer2.Deserialize<PackedByteArray>(ProtobufSerializer2.Serialize(p));
            });
        }

        [Test]
        public void UnpackedSerialize()
        {
            Assert.DoesNotThrow(() =>
            {
                ByteArray p = new ByteArray();
                ProtobufSerializer2.Serialize(p);
            });
        }

        [Test]
        public void UnpackedDeserialize()
        {
            Assert.DoesNotThrow(() =>
            {
                ByteArray p = new ByteArray();
                ProtobufSerializer2.Deserialize<ByteArray>(ProtobufSerializer2.Serialize(p));
            });
        }


        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator Test1WithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }

}
