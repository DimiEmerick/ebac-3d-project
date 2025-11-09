using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
