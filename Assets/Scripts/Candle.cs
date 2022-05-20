using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Candle : MonoBehaviour
{
    public AK.Wwise.Event AkEvent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        AkEvent.Post(gameObject);
        ++GameManager.Instance.CandlesCount;
        gameObject.SetActive(false);
    }
}
