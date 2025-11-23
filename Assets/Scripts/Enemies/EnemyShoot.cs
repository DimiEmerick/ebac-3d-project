using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;
        public string tagToAttack;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(tagToAttack))
            {
                gunBase.StartShoot();
                PlayAnimationByTrigger(Animation.AnimationType.ATTACK);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag(tagToAttack))
            {
                gunBase.StopShoot();
                PlayAnimationByTrigger(Animation.AnimationType.IDLE);
            }
        }

        protected override void OnKill()
        {
            base.OnKill();
            gunBase.StopShoot();
        }
    }
}
