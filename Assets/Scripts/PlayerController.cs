using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState 
    {
        Idle = 0,
        Walk,
        Run,
        Jump
    }

    public float Speed = 50.0f;
    public float JumpForce = 10.0f;
    public float AirJumpForce = 5.0f;
    public int MaxAirJump = 1;
    [Range(0, 90)]
    public float MaxFloorAngle = 45;

    public float DeathY;
    public float RespawnY;

    [Header("Public for Debug")]
    public PlayerState State = PlayerState.Idle;
    public float HorizontalAxis; 
    public bool JumpRequested = false;
    public bool IsGrounded = false;

    public bool LeftBlocking = false;
    public bool RightBlocking = false;
    public int AirJumpUsed = 0;

    private new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;
    private Animator animator;
    private float gravityScale;
    private ContactPoint2D[] contactPointsBuffer = new ContactPoint2D[8];

    private Vector3 spawningPosition;
    
    void Start()
    {
        spawningPosition = transform.position;

        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            JumpRequested = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire");
        }
    }

    void FixedUpdate()
    {
        // Ground Detection
        detectGround();

        // Movement
        float deltaX = HorizontalAxis * Speed * Time.fixedDeltaTime;
        if (IsGrounded || (deltaX > 0 && !RightBlocking) || (deltaX < 0 && !LeftBlocking))
        {
            rigidbody2D.velocity = new Vector2(deltaX, rigidbody2D.velocity.y);
        }

        if (IsGrounded)
        {
            if (Mathf.Abs(HorizontalAxis) > 0.8f)
            {
                setState(PlayerState.Run);
            }
            else if (Mathf.Abs(HorizontalAxis) > 0.01f)
            {
                setState(PlayerState.Walk);
            }
            else if (rigidbody2D.velocity.y < 0.01f)
            {
                setState(PlayerState.Idle);
            }
        }

        if (Mathf.Abs(deltaX) > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        // Jump
        if (JumpRequested)
        {
            if (IsGrounded)
            {
                rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                setState(PlayerState.Jump);
            }
            else if (AirJumpUsed < MaxAirJump)
            {
                AirJumpUsed += 1;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                rigidbody2D.AddForce(Vector2.up * AirJumpForce, ForceMode2D.Impulse);
                setState(PlayerState.Jump, true);
            }

            JumpRequested = false;
        }

        // Drop
        if (transform.position.y < RespawnY) 
        {
            rigidbody2D.MovePosition(spawningPosition);
            rigidbody2D.rotation = 0;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0;

            ++GameManager.Instance.DropCount;
        }
    }

    private void detectGround()
    {
        IsGrounded = false; 
        LeftBlocking = false;
        RightBlocking = false;

        const float epsilon = 0.1f;
        Vector2 min = collider2D.bounds.min;
        Vector2 max = collider2D.bounds.max;
        Vector2 corner1 = new Vector2(min.x, min.y - epsilon);
        Vector2 corner2 = new Vector2(max.x, min.y - epsilon * 2);
        var hit = Physics2D.OverlapArea(corner1, corner2);

        if (hit != null)
        {
            int nContacts = hit.GetContacts(contactPointsBuffer);
            Vector2 point = contactPointsBuffer[0].point;
            Vector2 dir = -contactPointsBuffer[0].normal;

            Debug.DrawRay(point.ToVector3(), dir.ToVector3() * 1, Color.yellow);
            if (nContacts >= 2)
            {
                Debug.DrawRay(contactPointsBuffer[1].point.ToVector3(), -contactPointsBuffer[1].normal.ToVector3() * 1, Color.grey);
            }

            float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (Mathf.Abs(90 - rotation) < MaxFloorAngle)
            {
                IsGrounded = true;
                AirJumpUsed = 0;
                if (Mathf.Abs(HorizontalAxis) > 0)
                {
                    rigidbody2D.rotation = rotation - 90;
                }
            }
            else 
            {
                if (dir.x > 0) 
                {
                    LeftBlocking = true;
                }
                else if (dir.x < 0)
                {
                    RightBlocking = true;
                }
            }
        } 
    }

    private void setState(PlayerState newState, bool forceReset = false)
    {
        if (!forceReset && newState == State)
        {
            return;
        }

        Debug.Log("Player State: " + State.ToString() + " -> " + newState.ToString());

        string animatorState = null;

        switch (newState)
        {
            case PlayerState.Idle:
                animatorState = "Idle";
                break;

            case PlayerState.Walk:
                animatorState = "Walk";
                break;

            case PlayerState.Run:
                animatorState = "Run";
                break;

            case PlayerState.Jump:
                animatorState = "Jump";
                break;

            default:
                animatorState = "Idle";
                break;
        }

        if (forceReset)
        {
            animator.Play(animatorState, -1, 0);
        }
        else
        {
            animator.Play(animatorState);
        }

        State = newState;
    }
}
