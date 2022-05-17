using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingBird : MonoBehaviour
{
    public Vector2 PushForce;
    public float DropMinY = -10;

    public Text RuneText;
    public GameObject[] EnablesOnRich;

    [Header("Debug")]
    public bool PushRequested;
    public bool ResetRequested;
    public int TargetRune;

    private new Rigidbody2D rigidbody2D;
    private Vector2 initialPos;
    private bool runeCollected;
    private int displayRune;

    void Start()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
        initialPos = new Vector2(transform.parent.position.x, transform.parent.position.y);
    }
    
    void Update()
    {
        if (displayRune < TargetRune)
        {
            displayRune += (int)(11111.0f * Time.deltaTime);
            if (displayRune > TargetRune)
            {
                displayRune = TargetRune;
            }

            RuneText.text = displayRune.ToString();
        }

        if (displayRune > 100000 && EnablesOnRich != null)
        {
            foreach(var go in EnablesOnRich)
            {
                go.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ShootingBall")
        {
            PushRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (!runeCollected && transform.parent.position.y < DropMinY)
        {
            collectRune();
        }

        if (PushRequested)
        {
            rigidbody2D.isKinematic = false;
            rigidbody2D.AddForce(PushForce, ForceMode2D.Impulse);
            PushRequested = false;
        }

        if (ResetRequested)
        {
            rigidbody2D.isKinematic = true;
            rigidbody2D.position = initialPos;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.rotation = 0;
            runeCollected = false;
            ResetRequested = false;
        }
    }

    private void collectRune()
    {
        runeCollected = true;
        TargetRune += 12000;
    }
}
