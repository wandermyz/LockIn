using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 50.0f;
    public float JumpForce = 10.0f;
    public float AirJumpForce = 5.0f;

    public int MaxAirJump = 1;

    [Header("Public for Debug")]
    public float HorizontalAxis; 
    public bool JumpRequested = false;
    public bool IsGrounded = false;
    public int AirJumpUsed = 0;

    private new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;
    private float gravityScale;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            JumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        detectGround();

        float deltaX = HorizontalAxis * Speed * Time.fixedDeltaTime;
        rigidbody2D.velocity = new Vector2(deltaX, rigidbody2D.velocity.y);

        if (JumpRequested)
        {
            if (IsGrounded)
            {
                rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            }
            else if (AirJumpUsed < MaxAirJump)
            {
                AirJumpUsed += 1;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                rigidbody2D.AddForce(Vector2.up * AirJumpForce, ForceMode2D.Impulse);
            }

            JumpRequested = false;
        }
    }

    private void detectGround()
    {
        const float epsilon = 0.1f;
        Vector2 min = collider2D.bounds.min;
        Vector2 max = collider2D.bounds.max;
        Vector2 corner1 = new Vector2(min.x, min.y - epsilon);
        Vector2 corner2 = new Vector2(max.x, min.y - epsilon * 2);
        var hit = Physics2D.OverlapArea(corner1, corner2);

        IsGrounded = (hit != null);
        
        if (IsGrounded)
        {
            AirJumpUsed = 0;
        }
    }
}
