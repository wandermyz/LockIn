using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

    public static T Instance => LazyInstance.Value;

    private static T CreateSingleton()
    {
        var existingComponent = GameObject.FindObjectOfType<T>();
        GameObject ownerObject;
        if (existingComponent == null)
        {
            ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
        }
        else
        {
            ownerObject = existingComponent.gameObject;
        }

        var instance = ownerObject.AddComponent<T>();
        DontDestroyOnLoad(ownerObject);
        return instance;
    }
}