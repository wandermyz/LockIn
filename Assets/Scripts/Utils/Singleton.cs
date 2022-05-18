using System;
using UnityEngine;

public abstract class SingletonBase : MonoBehaviour
{

}

public abstract class Singleton<T> : SingletonBase where T : MonoBehaviour
{
    private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

    public static T Instance => LazyInstance.Value;

    private static T CreateSingleton()
    {
        var existingComponent = GameObject.FindObjectOfType<T>();
        T instance;
        GameObject ownerObject;
        if (existingComponent == null)
        {
            ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
            instance = ownerObject.AddComponent<T>();
        }
        else
        {
            ownerObject = existingComponent.gameObject;
            instance = existingComponent;
        }

        DontDestroyOnLoad(ownerObject);
        return instance;
    }
}