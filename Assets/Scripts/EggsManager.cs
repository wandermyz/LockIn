using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsManager : MonoBehaviour
{
    public Transform[] Eggs;
    public float DroppedY = -10.0f;
    public GameObject[] Enables;

    private List<Transform> eggsList;

    void Start()
    {
        eggsList = new List<Transform>(Eggs);
    }

    void Update()
    {
        for (int i = 0; i < eggsList.Count; ++i)
        {
            Transform egg = eggsList[i];
            if (egg.position.y < DroppedY)
            {
                eggsList.RemoveAt(i);
                --i;
                Destroy(egg.gameObject);
            }
        }

        if (eggsList.Count == 0 && Enables != null)
        {
            foreach (var go in Enables)
            {
                go.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
