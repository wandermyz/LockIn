using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AkTriggerByTrigger : MonoBehaviour
{
    public string AkTrigger;

    [Header("Debug")]
    public bool Triggered;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        BGMManager.Instance.PostTrigger(AkTrigger);
        Triggered = true;
    }
}
