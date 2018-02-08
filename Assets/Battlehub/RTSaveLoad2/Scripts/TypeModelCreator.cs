using ProtoBuf.Meta;

namespace Battlehub.RTSaveLoad2
{
    public static partial class TypeModelCreator
    {
        public static RuntimeTypeModel Create()
        {
            RuntimeTypeModel model = TypeModel.Create();
            RegisterTypes(model);
            return model;
        }

        static partial void RegisterTypes(RuntimeTypeModel model);
    }
}

