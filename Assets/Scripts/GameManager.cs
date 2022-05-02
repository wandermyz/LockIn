using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int CandlesCount = 0;
    public int DropCount = 0;

    void Start()
    {
        // Force loading the instance if not already
        var instance = GameManager.Instance;
        Debug.Log("GameManager instance: " + instance);
    }
}
