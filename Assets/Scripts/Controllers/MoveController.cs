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
    [SerializeField] private float moveSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMaskToIgnore;
    private Rigidbody2D moveRb;
    private bool isInJump;
    private float horMove;
    
    // Start is called before the first frame update
    void Start()
    {
        moveRb = GetComponent<Rigidbody2D>();
        isInJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        horMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            OnMoveStart();
        if (( Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            OnMoveEnd();
    }
    private void FixedUpdate()
    {
        Vector2 vel = moveRb.velocity;
        vel.x += horMove;
        vel.x *= drag;
        moveRb.velocity = vel;
    }
    private void Jump()
    {
        // if is not in the air jump
        
        if (!isInJump)
        {
            moveRb.velocity += Vector2.up * jumpForce;
            StartCoroutine(waitForJump());
            if(OnJumpStart != null)
                OnJumpStart();
        }
    }
    
    private void HasLanded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layerMaskToIgnore);
        
        // if the distance between the player and the ground is less then 0.5 then make jump possible again
        
        if (hit && hit.distance < .5)
        {
            if (isInJump)
            {
                isInJump = false;
                if(OnJumpEnd != null)
                    OnJumpEnd();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HasLanded();
    }

    IEnumerator waitForJump()
    {
        yield return new WaitForEndOfFrame();
        isInJump = true;
        
    }
}













