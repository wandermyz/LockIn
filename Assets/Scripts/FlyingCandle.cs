using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCandle : MonoBehaviour
{
    public float Velocity;

    public float InitialFlameAngularVelocity;
    public float FlameAngularSpringK;
    public float FlameAngularSpringDrag;
    public float FlamePutoffRotationThreshold;
    public GameObject Flame;
    public int ID;

    [Header("Debug")]
    public bool Arrived;
    public bool DebugBlow;
    public bool DebugFlameReset;
    public float FlameRotation;
    public float FlameAngularVelocity;

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

    public void Blow(float amount, float delay)
    {
        StartCoroutine(doBlow(amount, delay));
    }

    void Start()
    {
        Flame = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (DebugBlow)
        {
            Blow(1.0f, 0);
            DebugBlow = false;
        }

        if (DebugFlameReset)
        {
            FlameRotation = 0;
            FlameAngularVelocity = 0;
            Flame.transform.rotation = Quaternion.identity;
            DebugFlameReset = false;
        }
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

        if (Flame.gameObject.activeSelf)
        {
            float angularForce = -FlameRotation * Mathf.Abs(FlameRotation) * FlameAngularSpringK - FlameAngularVelocity * FlameAngularSpringDrag;
            FlameAngularVelocity += angularForce * Time.fixedDeltaTime; 
            FlameRotation += FlameAngularVelocity * Time.fixedDeltaTime;
            Flame.transform.localRotation = Quaternion.Euler(0, 0, FlameRotation);

            if (Mathf.Abs(FlameRotation) > FlamePutoffRotationThreshold)
            {
                Flame.SetActive(false);
            }
        }
    }

    private IEnumerator doBlow(float amount, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        FlameAngularVelocity += InitialFlameAngularVelocity * amount * amount;
    }
}
