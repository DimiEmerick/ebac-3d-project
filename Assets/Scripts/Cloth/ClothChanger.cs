using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;
        public string shaderID = "_EmissionMap";

        private void Awake()
        {
            
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderID, texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.sharedMaterials[0].SetTexture(shaderID, setup.texture);
        }
    }
}
