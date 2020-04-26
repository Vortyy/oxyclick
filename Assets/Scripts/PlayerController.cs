using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 20f;
    public float jumpVelocity = 25f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        // Low jump multiplier and fall multiplier
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //else if (rb.velocity.y == 0)
        //{
            
        //}

        // Jump
        if (Input.GetKeyDown("space") && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            isGrounded = false;
        }

        // Move right and left
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Detect collision with floor
    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
