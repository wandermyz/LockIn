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
    public bool Shooting;
    public GameObject ShootingBall;
    public Vector2 ShootingLocalImpulse;
    public FlyingCandlesManager FlyingCandlesManager;

    public AK.Wwise.Event PawAkEvent;
    public AK.Wwise.Event ShootingAkEvent;

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

            if (Shooting && ShootingBall != null)
            {
                var newShootingBall = Instantiate(ShootingBall, transform.position, transform.rotation);
                Rigidbody2D shootingBallRididbody = newShootingBall.GetComponent<Rigidbody2D>();
                
                Vector3 localImpulse = ShootingLocalImpulse.ToVector3();
                Vector3 impulse = transform.TransformVector(localImpulse);
                shootingBallRididbody.AddForce(impulse.ToVector2(), ForceMode2D.Impulse);
            }

            FlyingCandle spawnedCandle = null;
            if (FlyingCandlesManager != null)
            {
                spawnedCandle = FlyingCandlesManager.SpawnOne(transform.position);
            }

            if (FlyingCandlesManager)
            {
                // Played by candle
            }
            else if (Shooting && ShootingBall != null)
            {
                ShootingAkEvent.Post(gameObject);
            }
            else
            {
                PawAkEvent.Post(gameObject);
            }
        }
    }
}
