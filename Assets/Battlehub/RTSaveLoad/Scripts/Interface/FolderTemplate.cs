using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTSaveLoad
{
    [System.Flags]
    public enum AssetTypeHint
    {
        Prefab = 1,
        Material = 2,
        ProceduralMaterial = 4,
        Mesh = 8,
        Texture = 16,

        All = Prefab | Material | ProceduralMaterial | Mesh | Texture,
    }

    [ExecuteInEditMode]
    public class FolderTemplate : MonoBehaviour
    {
        [Utils.EnumFlags]
        public AssetTypeHint TypeHint = AssetTypeHint.All;

        public Object[] Objects;
        private string m_name;

     
        private void Awake()
        {
            if(Application.isPlaying)
            {
                enabled = false;
            }
        }

        private void OnTransformParentChanged()
        {
            FixName();
        }

        private void Update()
        {
            if(m_name != name)
            {
                FixName();
                m_name = name;
            }
        }

        private void FixName()
        {
            FolderTemplate[] siblings = GetSiblings(this);

            name = PathHelper.RemoveInvalidFineNameCharacters(name);
             
            name = PathHelper.GetUniqueName(name, siblings.Select(s => s.name).ToArray());

            m_name = name;
        }

        private static FolderTemplate[] GetSiblings(FolderTemplate template)
        {
            if (template.transform.parent == null)
            {
                return new FolderTemplate[0];
            }

            FolderTemplate parentTemplate = template.transform.parent.GetComponent<FolderTemplate>();
            if (parentTemplate == null)
            {
                return new FolderTemplate[0];
            }

            List<FolderTemplate> children = new List<FolderTemplate>();
            foreach (Transform child in parentTemplate.transform)
            {
                FolderTemplate childTemplate = child.GetComponent<FolderTemplate>();
                if (childTemplate != null && childTemplate != template)
                {
                    children.Add(childTemplate);
                }
            }

            return children.ToArray();
        }



    }
}
