using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FallingBirdResetter : MonoBehaviour
{
    public float RequiredTimeSec = 3;
    public FallingBird Bird;

    [Header("Debug")]
    public bool PlayerInTrigger;

    private float elapsedTime;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        PlayerInTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player")        
        {
            return;
        }

        PlayerInTrigger = false;
        elapsedTime = 0;
    }

    void FixedUpdate()
    {
        if (PlayerInTrigger)
        {
            elapsedTime += Time.fixedDeltaTime;
        }

        if (elapsedTime > RequiredTimeSec)
        {
            Bird.ResetRequested = true;
        }
    }
}
