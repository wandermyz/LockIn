using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPaw : MonoBehaviour
{
    public enum PlayerPawState
    {
        Idle = 0,
        Fire,
        Back
    }

    public Vector2 Velocity;
    public float Distance;

    [Header("Debug")]
    public PlayerPawState State;

    private bool pawRequested;
    private new Rigidbody2D rigidbody2D;

    private Vector3 initialLocalPos;
    private Vector3 targetPos;
    private Animator animator;
    private int pawLayerIndex;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialLocalPos = transform.localPosition;
        animator = GetComponentInParent<Animator>();
        pawLayerIndex = animator.GetLayerIndex("Paw");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            pawRequested = true;
            animator.Play("Paw", pawLayerIndex, 0);
        }
    }

    void FixedUpdate()
    {
        if (pawRequested)
        {
            firePaw();
            pawRequested = false;
        }

        switch (State)
        {
            case PlayerPawState.Idle:
                rigidbody2D.simulated = false; 
                break;

            case PlayerPawState.Fire:
            {
                rigidbody2D.simulated = true; 
                Vector2 offset = Velocity * Time.fixedDeltaTime;
                transform.localPosition += offset.ToVector3();
                if ( (transform.localPosition - initialLocalPos).sqrMagnitude > Distance * Distance )
                {
                    targetPos = transform.localPosition;
                    State = PlayerPawState.Back;
                }
                break;
            }

            case PlayerPawState.Back:
            {
                rigidbody2D.simulated = false; 
                Vector2 offset = Velocity * Time.fixedDeltaTime;
                transform.localPosition -= offset.ToVector3();

                if (Vector3.Dot(transform.localPosition - initialLocalPos, targetPos - initialLocalPos) < 0)
                {
                    State = PlayerPawState.Idle;
                    transform.localPosition = initialLocalPos;
                }
                break;
            }
        }
    }

    private void firePaw()
    {
        if (State == PlayerPawState.Idle)    
        {
            State = PlayerPawState.Fire;
        }
    }
}
