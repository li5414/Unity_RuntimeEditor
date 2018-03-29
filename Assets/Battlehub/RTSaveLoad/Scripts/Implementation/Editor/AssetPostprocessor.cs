using UnityEngine;
using System.Linq;

namespace Battlehub.RTSaveLoad
{
    /* Commented because presumably causes problems on certain environments
    public class RTSaveLoadAssetPosprocessor : UnityEditor.AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {

            if (!ResourceMapGen.Automatic)
            {
                return;
            }

            if (ResourceMapGen.IsBusy)
            {
                return;
            }


            if (importedAssets.Length == 0 && deletedAssets.Length == 0)
            {
                return;
            }

            if (importedAssets.Any(a => !string.IsNullOrEmpty(a) &&
                (a.Contains(IdentifiersMap.ResourceMapPrefabName) || a.Contains("RTSaveLoad") && a.Contains("ShaderInfo") && a.EndsWith(".txt"))) ||

              deletedAssets.Any(a => !string.IsNullOrEmpty(a) &&
                (a.Contains(IdentifiersMap.ResourceMapPrefabName) || a.Contains("RTSaveLoad") && a.Contains("ShaderInfo") && a.EndsWith(".txt"))) ||

              movedAssets.Any(a => !string.IsNullOrEmpty(a) &&
                (a.Contains(IdentifiersMap.ResourceMapPrefabName) || a.Contains("RTSaveLoad") && a.Contains("ShaderInfo") && a.EndsWith(".txt"))))
            {
                return;
            }

            if (importedAssets.All(a => a == null || !string.IsNullOrEmpty(a) && (
                    a.EndsWith("ProjectSettings.asset") ||
                    a.EndsWith(".unity") ||
                    a.EndsWith(".cs")))
                    &&
                deletedAssets.All(a => a == null || !string.IsNullOrEmpty(a) && (
                    a.EndsWith("ProjectSettings.asset") ||
                    a.EndsWith(".unity") ||
                    a.EndsWith(".cs")))
                    &&
                movedAssets.All(a => a == null || !string.IsNullOrEmpty(a) && (
                    a.EndsWith("ProjectSettings.asset") ||
                    a.EndsWith(".unity") ||
                    a.EndsWith(".cs"))))
            {
                return;
            }

            Debug.Log("Resource Map Auto-Update");
            ResourceMapGen.CreateResourceMap(false);

        }
         
}
   */
}

