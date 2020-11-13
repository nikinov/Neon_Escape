using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public delegate void MoveStartAction();
    public static event MoveStartAction OnMoveStart;
    public delegate void MoveEndAction();
    public static event MoveEndAction OnMoveEnd;
    public delegate void JumpStartAction();
    public static event JumpStartAction OnJumpStart;
    public delegate void JumpEndAction();
    public static event JumpEndAction OnJumpEnd;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _drag;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _layerMaskToIgnore;
    private Rigidbody2D _moveRb;
    private bool _isInJump;
    private float _horMove;
    
    // Start is called before the first frame update
    void Start()
    {
        _moveRb = GetComponent<Rigidbody2D>();
        _isInJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        _horMove = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && OnMoveStart != null)
            OnMoveStart();
        if (( Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && OnMoveEnd != null)
            OnMoveEnd();
    }
    private void FixedUpdate()
    {
        Vector2 vel = _moveRb.velocity;
        vel.x += _horMove;
        vel.x *= _drag;
        _moveRb.velocity = vel;
    }
    private void Jump()
    {
        // if is not in the air jump
        
        if (!_isInJump)
        {
            _moveRb.velocity += Vector2.up * _jumpForce;
            StartCoroutine(waitForJump());
            if(OnJumpStart != null)
                OnJumpStart();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HasLanded();
    }

    private void HasLanded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, _layerMaskToIgnore);
        
        // if the distance between the player and the ground is less then 0.5 then make jump possible again
        
        if (hit && hit.distance < 1.5)
        {
            if (_isInJump)
            {
                _isInJump = false;
                if(OnJumpEnd != null)
                    OnJumpEnd();
            }
        }
    }

    IEnumerator waitForJump()
    {
        yield return new WaitForEndOfFrame();
        _isInJump = true;
        
    }
}













