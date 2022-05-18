using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCandle : MonoBehaviour
{
    public float Velocity;

    [Header("Debug")]
    public bool Arrived;

    public GameObject Flame;

    private Vector3 spawnedPosition;
    private Vector3 targetPosition;
    private Vector3 flyingDir;

    public void Spawn(Vector3 position)
    {
        targetPosition = transform.position;
        spawnedPosition = position;
        transform.position = position;

        flyingDir = (targetPosition - spawnedPosition).normalized;

        gameObject.SetActive(true);
    }

    public void LightUp()
    {
        Flame.SetActive(true);
    }

    void Start()
    {
        Flame = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        if (!Arrived)
        {
            Vector3 newPosition = transform.position + flyingDir * Velocity * Time.fixedDeltaTime; 

            if (Vector3.Dot(newPosition - spawnedPosition, targetPosition - newPosition) < 0)
            {
                transform.position = targetPosition; 
                Arrived = true;
            }
            else
            {
                transform.position = newPosition;
            }
        }
    }

    void Update()
    {
        
    }
}
