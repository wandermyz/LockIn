using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSpawner : MonoBehaviour
{
    public SingletonBase[] Prefabs;

    void Start()
    {
        foreach (SingletonBase b in Prefabs)
        {
            var existingComponent = GameObject.FindObjectOfType(b.GetType());
            if (existingComponent == null)
            {
                var instance = Instantiate(b.gameObject);
                DontDestroyOnLoad(instance);
            }
        }
    }
}
