using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(GroundDetector))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private GroundDetector _groundDetector;
    private Vector3 _direction;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        StartJump();
        Movement();
    }

    private void Movement()
    {
        _direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            _direction = Vector3.left;
            if (_direction.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = Vector3.right;
            if (_direction.x > 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        _direction *= _speed;
        _direction.y = _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = _direction;
        _animator.SetFloat("Speed", Mathf.Abs(_direction.x));
    }

    private void StartJump()
    {
        if (Input.GetKey(KeyCode.W) && _groundDetector.IsGrounded == true)
        {
            Debug.Log("Jump!");
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            //_animator.SetTrigger(Jump);
        }
    }
}
