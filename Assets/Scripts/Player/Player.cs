using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.StateMachine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float forceJump = 3f;
    public Rigidbody playerRB;
    public Animator playerAnimator;

    private float _currentSpeed;

    public void Walk()
    {
        Vector3 direction = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
            direction += Vector3.forward;
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;
        if(direction != Vector3.zero)
        {
            direction = direction.normalized;
            transform.position += direction * _currentSpeed * Time.deltaTime;
        }
    }

    public void Jump()
    {
            playerRB.velocity = Vector3.up * forceJump;
    }

    private void Start()
    {
        _currentSpeed = speed;
    }
}
