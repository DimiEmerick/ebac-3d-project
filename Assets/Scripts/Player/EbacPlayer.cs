using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbacPlayer : MonoBehaviour//, IDamageable
{
    public List<Collider> colliders;
    public Animator playerAnimator;
    public CharacterController characterController;
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
    public UIFillUpdater uiGunUpdater;

    private float _vSpeed = 0f;
    private bool _alive = true;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
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
        }
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
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
            _vSpeed = 0;
            if(Input.GetKeyDown(jumpKeyCode))
            {
                _vSpeed = jumpSpeed;
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
}
