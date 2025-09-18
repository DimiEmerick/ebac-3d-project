using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;
        public FlashColor flashColor;
        public ParticleSystem particleSystem;
        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [SerializeField] private AnimationBase _animationBase;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithBornAnimation)
                BornAnimation();
        }
        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill() 
        {
            OnKill(); 
        }

        protected virtual void OnKill() 
        {
            if (enemyCollider != null) enemyCollider.enabled = false;
            PlayAnimationByTrigger(AnimationType.DEATH);
            Destroy(gameObject, 1.45f);
        }

        protected virtual void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(15);
            _currentLife -= f;
            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
    }
}
