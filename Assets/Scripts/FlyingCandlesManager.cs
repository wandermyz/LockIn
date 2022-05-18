using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCandlesManager : MonoBehaviour
{
    public enum FlyingCandlesState
    {
        Idle = 0,
        Dimming,
        LightingUp,
        Done
    }

    public BoxCollider2D ActiveArea;
    public SpriteRenderer[] DimSprites;

    public GameObject[] EnablesOnLightUp;

    [Range(0, 1)]
    public float DimmedAlpha = 0.8f;

    public float DimVelocity = 0.2f;

    public float LightingUpInterval = 0.2f;
    public float StateInterval = 0.5f;

    [Header("Debug")]
    public FlyingCandlesState State;

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

        if (PendingCandles.Count == 0)
        {
            StartCoroutine(lightUp());
        }
    }

    void Start()
    {
        var candles = GetComponentsInChildren<FlyingCandle>(true);
        PendingCandles = new List<FlyingCandle>(candles);
    }

    void Update()
    {
    }

    private IEnumerator lightUp()
    {
        State = FlyingCandlesState.Dimming;

        yield return new WaitForSeconds(StateInterval);

        float alpha = 1.0f; 

        while (alpha > DimmedAlpha)
        {
            alpha -= DimVelocity * Time.deltaTime;

            foreach(var s in DimSprites)
            {
                s.color = new Color(s.color.r, s.color.g, s.color.b, alpha);
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(StateInterval);

        State = FlyingCandlesState.LightingUp;

        foreach (var c in SpawnedCandles)
        {
            c.LightUp();
            yield return new WaitForSeconds(LightingUpInterval);
        }

        yield return new WaitForSeconds(StateInterval);

        State = FlyingCandlesState.Done;

        if (EnablesOnLightUp != null)
        {
            foreach (var go in EnablesOnLightUp)
            {
                go.SetActive(true);
            }
        }
    }

    public void DebugSpawnAll()
    {
        foreach (var c in PendingCandles)
        {
            c.Spawn(Vector3.zero);
            SpawnedCandles.Add(c);
        }

        PendingCandles.Clear();

        StartCoroutine(lightUp());
    }
}
