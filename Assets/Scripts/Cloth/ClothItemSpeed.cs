using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float targetSpeed = 20f;

        public override void Collect()
        {
            clothText = "Speed Cloth";
            base.Collect();
            EbacPlayer.Instance.ChangeSpeed(targetSpeed, duration);
        }
    }
}
