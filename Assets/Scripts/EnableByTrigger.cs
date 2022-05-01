using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnableByTrigger : MonoBehaviour
{
    public int RequiredCount = 1; 
    public GameObject[] Enables;

    [Header("Debug")]
    public int TriggeredCount = 0;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") {
            return;
        }
        
        ++TriggeredCount;

        if (TriggeredCount >= RequiredCount && Enables != null)
        {
            foreach (GameObject go in Enables)
            {
                go.SetActive(true);
            }
        }
    }
}
