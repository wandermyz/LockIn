using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedEnable : MonoBehaviour
{
    public float Delay;
    public GameObject[] Enables;
    void Start()
    {
        StartCoroutine(enables());        
    }

    private IEnumerator enables()
    {
        yield return new WaitForSeconds(Delay);
        if (Enables != null)
        {
            foreach (var go in Enables)
            {
                go.SetActive(true);
            }
        }
        Destroy(gameObject);
    }
}
