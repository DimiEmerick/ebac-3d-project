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
        public ParticleSystem enemyParticleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [SerializeField] private float _currentLife;
        private EbacPlayer _player;

        [Header("Start Animation")]
        public bool startWithBornAnimation = true;
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;

        [SerializeField] private AnimationBase _animationBase;


        #region M�TODOS UNITY
        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<EbacPlayer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            EbacPlayer p = collision.transform.GetComponent<EbacPlayer>();
            if (p != null)
            {
                p.Damage(1);
            }
        }

        public virtual void Update()
        {
            if(lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
        #endregion

        #region M�TODOS PROTECTED VIRTUAL
        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation)
                BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (enemyCollider != null) enemyCollider.enabled = false;
            enemyParticleSystem.Emit(50);
            PlayAnimationByTrigger(AnimationType.DEATH);
            Destroy(gameObject, 1.45f);
        }

        protected virtual void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (enemyParticleSystem != null) enemyParticleSystem.Emit(15);
            _currentLife -= f;
            if (_currentLife <= 0)
            {
                Kill();
            }
        }
        #endregion

        #region M�TODOS P�BLICOS
        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 direction)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - direction, .1f);
        }
        #endregion

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

        protected void ResetLife()
        {
            _currentLife = startLife;
        }
    }
}
