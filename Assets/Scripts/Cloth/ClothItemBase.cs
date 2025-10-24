using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration = 2f;
        public string compareTag = "Player";
        public string clothText = "Base Cloth";

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("Collect");
            var setup = ClothManager.Instance.GetSetupByType(clothType);
            EbacPlayer.Instance.ChangeTexture(setup, duration);
            EbacPlayer.Instance.ShowText(clothText);
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
