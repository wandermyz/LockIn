using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAirJump : MonoBehaviour
{
    public PlayerController PlayerController;
    void OnTriggerEnter2D(Collider2D other)
    {
        ++PlayerController.MaxAirJump;
        gameObject.SetActive(false);
    }
}
