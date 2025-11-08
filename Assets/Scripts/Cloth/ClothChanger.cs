using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;
        public Texture2D defaultTexture;
        public string shaderID = "_EmissionMap";

        private void Awake()
        {
            defaultTexture = (Texture2D)mesh.sharedMaterials[0].GetTexture(shaderID);
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

        public void ResetTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderID, defaultTexture);
        }
    }
}
