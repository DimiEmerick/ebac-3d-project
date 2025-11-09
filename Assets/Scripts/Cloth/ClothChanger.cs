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

        [NaughtyAttributes.Button]
        public void ChangeTexture(Texture2D texture)
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
