#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Battlehub.RTSaveLoad
{

    public class CreateAssetBundles
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Build AssetBundles")]
#endif
        public static void BuildAllAssetBundles()
        {
#if UNITY_EDITOR
            if(!AssetDatabase.IsValidFolder("Assets/StreamingAssets"))
            {
                AssetDatabase.CreateFolder("Assets", "StreamingAssets");
            }

            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None , EditorUserBuildSettings.activeBuildTarget);
#endif
        }
    }


}
