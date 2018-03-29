using UnityEngine;

namespace Battlehub.RTSaveLoad
{
    public delegate void AssetBundleEventHandler(string bundleName, AssetBundle bundle);
    public interface IAssetBundleLoader
    {
        void Load(string name, AssetBundleEventHandler callback);
    }

    public class AssetBundleLoader : IAssetBundleLoader
    {
        public void Load(string name, AssetBundleEventHandler callback)
        {
            if(!System.IO.File.Exists(Application.streamingAssetsPath + "/" + name))
            {
                callback(name, null);
                return;
            }
            AssetBundle bundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + name);
            if(callback != null)
            {
                callback(name, bundle);
            }
        }
    }

}
