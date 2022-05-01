using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnableByTrigger : MonoBehaviour
{
    public int RequiredCount = 1; 
    public bool Repeatable = false;
    public GameObject[] Enables;

    public SpriteCounter Counter;

    [Header("Debug")]
    public int TriggeredCount = 0;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") {
            return;
        }
        
        ++TriggeredCount;
        
        if (TriggeredCount > 0 && Counter != null)
        {
            Counter.Value = (RequiredCount - TriggeredCount);
            Counter.gameObject.SetActive(true);
        }

        if (TriggeredCount >= RequiredCount && Enables != null)
        {
            if (Counter != null)
            {
                Counter.gameObject.SetActive(false);
            }

            foreach (GameObject go in Enables)
            {
                go.SetActive(true);
            }

            if (!Repeatable)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
