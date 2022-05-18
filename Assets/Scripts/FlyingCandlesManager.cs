using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCandlesManager : MonoBehaviour
{
    public BoxCollider2D ActiveArea;

    private List<FlyingCandle> PendingCandles;
    private List<FlyingCandle> SpawnedCandles = new List<FlyingCandle>();

    public void SpawnOne(Vector3 position)
    {
        if (PendingCandles == null || PendingCandles.Count == 0)
        {
            return;
        }

        if (!ActiveArea.OverlapPoint(position.ToVector2()))
        {
            return;
        }

        int i = Random.Range(0, PendingCandles.Count - 1);
        FlyingCandle c = PendingCandles[i];
        PendingCandles.RemoveAt(i);
        SpawnedCandles.Add(c);

        c.Spawn(position);
    }

    void Start()
    {
        var candles = GetComponentsInChildren<FlyingCandle>(true);
        PendingCandles = new List<FlyingCandle>(candles);
    }

    void Update()
    {
        
    }
}
