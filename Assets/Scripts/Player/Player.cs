using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float forceJump = 3f;
    public Rigidbody playerRB;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.velocity = Vector3.up * forceJump;
        }
    }

    private void Start()
    {
        _currentSpeed = speed;
    }

    private void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.stateMachine.CurrentStateName == GameManager.GameStates.GAMEPLAY)
        {
            Walk();
            Jump();
        }
        else
        {
            playerRB.velocity = Vector3.zero;
        }
    }
}
