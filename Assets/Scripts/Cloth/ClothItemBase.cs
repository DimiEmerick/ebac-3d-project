using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string compareTag = "Player";

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
            EbacPlayer.Instance.clothChanger.ChangeTexture(setup);
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
