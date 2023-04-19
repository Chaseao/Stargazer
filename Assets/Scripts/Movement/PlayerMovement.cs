using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{

    private new Rigidbody2D rigidbody;
    private Vector2 movementForce;
    private Vector2 playerInput;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float speed = 0.25f;
    [SerializeField] private Animator playerAnimation;
    private Direction direction = Direction.Left;

    private enum Direction
    {
        Right,
        Left
    }

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        Physics2D.gravity = Vector2.zero;
        rigidbody.velocity = movementForce;

        switch (direction)
        {
            case Direction.Left:
                if (playerInput.x > 0)
                {
                    direction = Direction.Right;
                    playerAnimation.SetBool("IsLeft", true);
                    playerAnimation.SetBool("IsRight", false);
                }

                break;
            case Direction.Right:
                if (playerInput.x < 0)
                {
                    direction = Direction.Left;
                    playerAnimation.SetBool("IsLeft", false);
                    playerAnimation.SetBool("IsRight", true);
                }

                break;
        }
        if(rigidbody.velocity != Vector2.zero)
        {
            playerAnimation.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimation.SetBool("IsWalking", false);
        }

    }

    private void OnEnable()
    {
        Controller.OnMove += Move;
    }

    private void OnDisable()
    {
        Controller.OnMove -= Move;
    }

    void Move(Vector2 input)
    {
        if(!canMove)
        {
            input = Vector2.zero;
        }
        playerInput= input;
        input *= speed;
        movementForce = new Vector2(input.x, input.y);

    }


}
