using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;
using Cloth;

public class EbacPlayer : Singleton<EbacPlayer> //, IDamageable
{
    public List<Collider> colliders;
    public Animator playerAnimator;
    public CharacterController characterController;
    public TextMeshProUGUI notificationText;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;
    public List<UIFillUpdater> uiGunUpdaters;

    [SerializeField] private ClothChanger _clothChanger;
    private float _vSpeed = 0f;
    private bool _alive = true;
    private bool _jumping = false;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    #region LIFE
    private void OnKill(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            playerAnimator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        playerAnimator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
    }

    public void Damage(float damage, Vector3 direction)
    {
        // Damage(damage);
    }
    #endregion

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded)
        {
            if(_jumping)
            {
                _jumping = false;
                playerAnimator.SetTrigger("Land");
            }
            _vSpeed = 0;
            if(Input.GetKeyDown(jumpKeyCode))
            {
                _vSpeed = jumpSpeed;
                if(!_jumping)
                {
                    _jumping = true;
                    playerAnimator.SetTrigger("Jump");
                }
            }
        }

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;


        playerAnimator.SetBool("Run", inputAxisVertical != 0);

        var isWalking = inputAxisVertical != 0;
        if(isWalking)
        {
            if(Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                playerAnimator.speed = speedRun;
            }
            else
            {
                playerAnimator.speed = 1;
            }
        }
        characterController.Move(speedVector * Time.deltaTime);
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint()) 
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

    public void ShowText(string text)
    {
        StartCoroutine(NotificationText(text));
    }

    IEnumerator NotificationText(string text)
    {
        notificationText.text = text;
        yield return new WaitForSeconds(2f);
        notificationText.text = "";
    }
}
