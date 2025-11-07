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
        public Texture2D currentTexture;
        public string shaderID = "_EmissionMap";


        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            currentTexture = mesh.material.mainTexture as Texture2D;
            mesh.sharedMaterials[0].SetTexture(shaderID, texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.sharedMaterials[0].SetTexture(shaderID, setup.texture);
        }

        public void ResetTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderID, defaultTexture);
            currentTexture = mesh.material.mainTexture as Texture2D;
        }
    }
}
