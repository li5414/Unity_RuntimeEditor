using System;
namespace Battlehub.RTSaveLoad
{
    public interface ISerializer
    {
        byte[] Serialize<TData>(TData data);
        TData Deserialize<TData>(byte[] data);
        TData DeepClone<TData>(TData data);
    }
}

