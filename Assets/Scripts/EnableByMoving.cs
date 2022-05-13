using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnableByMoving : MonoBehaviour
{
    public GameObject[] Enables;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Enables != null && Mathf.Abs(rigidbody2D.velocity.x) > 0.1f)
        {
            foreach (GameObject go in Enables)
            {
                go.SetActive(true);
            }
        }
    }
}
