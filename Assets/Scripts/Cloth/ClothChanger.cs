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

        public void ChangeTexture(Texture2D texture)
        {
            mesh.sharedMaterial.SetTexture(shaderID, texture);
            mesh.sharedMaterial.SetTexture("_MainTex", texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.sharedMaterial.SetTexture(shaderID, setup.texture);
            mesh.sharedMaterial.SetTexture("_MainTex", setup.texture);
        }

        [NaughtyAttributes.Button]
        public void ResetTexture()
        {
            mesh.sharedMaterial.SetTexture(shaderID, defaultTexture);
            mesh.sharedMaterial.SetTexture("_MainTex", defaultTexture);
        }
    }
}
