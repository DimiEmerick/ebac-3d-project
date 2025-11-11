using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        //  public SFXType sfxType;
        public ItemType itemType;
        public string compareTag = "Player";
        public float timeToHide = 3f;
        public ParticleSystem itemParticleSystem;
        public GameObject graphicItem;
        public Collider triggerItem;
        public Collider colliderItem;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        /* private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        } */

        protected virtual void Collect()
        {
            //  PlaySFX();
            var itemSetup = ItemManager.Instance.GetItemByType(itemType);
            ItemManager.Instance.PlaySFX(itemSetup);
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            if (colliderItem != null) colliderItem.enabled = false;
            triggerItem.enabled = false;
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (itemParticleSystem != null) itemParticleSystem.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}