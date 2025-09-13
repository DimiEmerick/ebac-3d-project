using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.StateMachine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float forceJump = 3f;
    public float turnSpeed = 250f;
    public Rigidbody playerRB;
    public Animator playerAnimator;

    private float _currentSpeed;

    [Header("Ground Check")]
    public float groundDistance = .25f;        // raio do check
    public Transform groundCheck;              // um vazio posicionado no pé do personagem
    public LayerMask groundMask;               // layer que representa o chão
    
    private bool _isGrounded;

    public void Walk()
    {
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();
        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += camForward;
        if (Input.GetKey(KeyCode.A))
            direction -= camRight;
        if (Input.GetKey(KeyCode.S))
            direction -= camForward;
        if (Input.GetKey(KeyCode.D))
            direction += camRight;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _currentSpeed = speed * 2;
            playerAnimator.speed = 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _currentSpeed = speed;
            playerAnimator.speed = 1;
        }

        if (direction != Vector3.zero)
        {
            direction = direction.normalized;
            transform.position += _currentSpeed * Time.deltaTime * direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        } 
    }

    public void Jump()
    {
        if(_isGrounded)
            playerRB.velocity = Vector3.up * forceJump;
    }

    private void Start()
    {
        _currentSpeed = speed;
        playerAnimator.speed = 1;
    }

    private void Update()
    {
        // posiciona uma esfera invisível que checa se o personagem está tocando o chão
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); 
    }
}
