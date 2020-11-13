using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMaskToIgnore;
    private Rigidbody2D moveRb;
    private bool isInJump;
    
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
    }
    private void FixedUpdate()
    {
        float horMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        Vector2 vel = moveRb.velocity;
        vel.x += horMove;
        vel.x *= drag;
        moveRb.velocity = vel;
    }
    private void Jump()
    {
        if (!isInJump)
        {
            moveRb.velocity += Vector2.up * jumpForce;
            StartCoroutine(waitForJump());
        }
    }
    
    private void HasLanded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layerMaskToIgnore);
        if (hit && hit.distance < .5)
        {
            if (isInJump)
                isInJump = false;
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













