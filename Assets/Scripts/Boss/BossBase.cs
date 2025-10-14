using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;
using Animation;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }
    public class BossBase : MonoBehaviour, IDamageable
    {
        public ParticleSystem bossParticleSystem;
        public HealthBase healthBase;
        public EbacPlayer player;
        public FlashColor flashColor;
        public BossTrigger bossTrigger;

        [Header("Animation")]
        public AnimationBase animationBase;
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> waypoints;

        [Header("Attack")]
        public GunBase gunBase;
        public int attackAmount = 5;
        public float timeBetweenAttacks = .2f;

        private StateMachine<BossAction> _stateMachine;

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            OnValidate();
            Init();
            if(healthBase != null) healthBase.OnKill += OnBossKill;
            player = GameObject.FindObjectOfType<EbacPlayer>();
            bossTrigger = GameObject.FindObjectOfType<BossTrigger>();
        }

        private void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();
            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            _stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            _stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
            SwitchState(BossAction.INIT);
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
            Destroy(gameObject, 1.45f);
            bossTrigger.TurnCameraOff();
        }

        public void Damage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            healthBase.Damage(damage);
            if (bossParticleSystem != null) bossParticleSystem.Emit(15);
        }
        public void Damage(float damage, Vector3 direction)
        {
            if (flashColor != null) flashColor.Flash();
            healthBase.Damage(damage);
            if (bossParticleSystem != null) bossParticleSystem.Emit(15);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                gunBase.StartShoot();
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                transform.LookAt(player.transform.position);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            EbacPlayer p = collision.transform.GetComponent<EbacPlayer>();
            if (p != null)
            {
                p.healthBase.Damage(1);
            }
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                transform.LookAt(t.position);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
            SwitchState(BossAction.WALK);
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            _stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}
