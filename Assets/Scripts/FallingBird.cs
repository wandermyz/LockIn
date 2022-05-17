using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBird : MonoBehaviour
{
    public Vector2 PushForce;

    [Header("Debug")]
    public bool PushRequested;
    public bool ResetRequested;

    private new Rigidbody2D rigidbody2D;
    private Vector2 initialPos;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (PushRequested)
        {
            rigidbody2D.AddForce(PushForce, ForceMode2D.Impulse);
            PushRequested = false;
        }

        if (ResetRequested)
        {
            rigidbody2D.MovePosition(initialPos);
            ResetRequested = false;
        }
    }
}
