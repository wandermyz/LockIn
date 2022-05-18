using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablesByAnimationEvent : MonoBehaviour
{
    public GameObject[] Enables;

    public void OnAnimationFinished()
    {
        if (Enables == null)
        {
            return;
        }

        foreach (var go in Enables)
        {
            go.SetActive(true);
        }
    }
}
