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
    private float _sprintMultiplier = 2;
    private GroundDetector _groundDetector;
    private Vector3 _direction;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private float _currentSpeed;
    private bool _isLedderIsNear;
    private bool _isClimbing;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
        _isClimbing = false;
    }

    private void FixedUpdate()
    {
        Movement();
        CheckFalling();
    }

    private void Movement()
    {
        if (_isClimbing == false)
        {
            _currentSpeed = _speed;
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

            if (Input.GetKey(KeyCode.Space))
            {
                _animator.SetTrigger("StartJump");
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed *= _sprintMultiplier;
            }

            _direction *= _currentSpeed;
            _direction.y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = _direction;
            _animator.SetFloat("Speed", Mathf.Abs(_direction.x));
            
            if (Input.GetKeyUp(KeyCode.E) && _isLedderIsNear) // Check for E key press near ladder
            {
                _animator.SetBool("IsClimbing", true);
                _isClimbing = true;
                Debug.Log("Stick");
            }
        }
        else
        {
            Climbing();
        }
    }

    private void Climbing()
    {
        _rigidbody2D.gravityScale = 0f;
        if (Input.GetKeyUp(KeyCode.E)) // Detach from ladder on E press
        {
            StopClimbing();
        }
        else if (Input.GetKeyDown(KeyCode.W)) // Climb up on W press
        {
            _rigidbody2D.velocity = new Vector2(0f, _speed);
            _animator.SetFloat("ClimbSpeed", _speed);
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Climb up on W press
        {
            _rigidbody2D.velocity = new Vector2(0f, _speed);
            _animator.SetFloat("ClimbSpeed", _speed);
        }
    }

    private void StopClimbing()
    {
        _animator.SetBool("IsClimbing", false);
        _isClimbing = false;
        _rigidbody2D.gravityScale = 1f;
    }

    private void CheckFalling()
    {
        _animator.SetBool("IsGrounded", _groundDetector.IsGrounded);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _animator.SetBool("StartJump", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Ledder>())
        {
            _isLedderIsNear = true;
            Debug.Log("Ledger is near");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Ledder>())
        {
            _isLedderIsNear = false;
            Debug.Log("Exit ledder");
            StopClimbing();
        }
    }
}
