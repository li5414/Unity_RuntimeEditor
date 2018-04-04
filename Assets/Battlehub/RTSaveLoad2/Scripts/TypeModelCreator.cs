using ProtoBuf.Meta;

namespace Battlehub.RTSaveLoad2
{
    public static partial class TypeModelCreator
    {
        public static RuntimeTypeModel Create()
        {
            RuntimeTypeModel model = TypeModel.Create();
            RegisterAutoTypes(model);
            RegisterUserDefinedTypes(model);
            return model;
        }

        static partial void RegisterAutoTypes(RuntimeTypeModel model);

        static partial void RegisterUserDefinedTypes(RuntimeTypeModel model);
    }

    //public static partial class TypeModelCreator
    //{
    //    static partial void RegisterAutoTypes(RuntimeTypeModel model)
    //    {
            
    //      //  model.Add(typeof(TestData1), false). AddSubType(500, typeof(TestData2)).SetSurrogate()
    //    }
    //}
}

