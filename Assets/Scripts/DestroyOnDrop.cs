using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDrop : MonoBehaviour
{
    public float DropMinY = -10.0f;
    public float Lifetime = 0;

    private float spawnedTime;

    void Start()
    {
        spawnedTime = Time.fixedTime;
    }

    void FixedUpdate()
    {
        if (transform.position.y < DropMinY)
        {
            Destroy(gameObject);
        }
        else if (Lifetime > 0 && Time.fixedTime - spawnedTime > Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
