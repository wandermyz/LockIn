using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAirJump : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject[] Enables;
    void OnTriggerEnter2D(Collider2D other)
    {
        ++PlayerController.MaxAirJump;

        if (Enables != null)
        {
            foreach (var go in Enables)
            {
                go.SetActive(true);
            }
        }

        gameObject.SetActive(false);
    }
}
