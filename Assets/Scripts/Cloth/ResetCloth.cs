using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cloth
{
    public class ResetCloth : MonoBehaviour
    {
        private EbacPlayer player;

        private void Awake()
        {
            player = FindObjectOfType<EbacPlayer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            player.clothChanger.ResetTexture();
        }

        private void OnEnable()
        {
            transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack).From();
        }
    }
}
