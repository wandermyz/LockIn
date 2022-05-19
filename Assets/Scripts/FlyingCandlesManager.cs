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
        Animation,
        Blowing,
        Undimming,
        Done
    }

    public BoxCollider2D ActiveArea;
    public SpriteRenderer[] DimSprites;

    public Animator HBAnim;
    public BlowController BlowController;

    public GameObject SecretPlatforms;

    [Range(0, 1)]
    public float DimmedAlpha = 0.8f;

    public float DimVelocity = 0.2f;

    public float LightingUpInterval = 0.2f;
    public float StateInterval = 0.5f;

    [Header("Debug")]
    public FlyingCandlesState State;

    private List<FlyingCandle> PendingCandles;
    private List<FlyingCandle> SpawnedCandles;

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
        SpawnedCandles[c.ID] = c;

        c.Spawn(position);

        if (PendingCandles.Count == 0)
        {
            StartCoroutine(lightUp());
        }
    }

    void Start()
    {
        var candles = GetComponentsInChildren<FlyingCandle>(true);
        PendingCandles = new List<FlyingCandle>();
        SpawnedCandles = new List<FlyingCandle>();
        for (int i = 0; i < candles.Length; ++i)
        {
            var c = candles[i];
            c.ID = i;
            PendingCandles.Add(c);
            SpawnedCandles.Add(null);
        }
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

        GetComponent<AkState>().data.SetValue();

        yield return new WaitForSeconds(StateInterval);

        State = FlyingCandlesState.LightingUp;

        foreach (var c in SpawnedCandles)
        {
            c.LightUp();
            yield return new WaitForSeconds(LightingUpInterval);
        }

        yield return new WaitForSeconds(StateInterval);

        State = FlyingCandlesState.Animation;

        HBAnim.gameObject.SetActive(true);

        while (!HBAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(StateInterval);

        State = FlyingCandlesState.Blowing;

        BlowController.Candles = SpawnedCandles;
        BlowController.gameObject.SetActive(true);

        while (!BlowController.IsDone)
        {
            yield return new WaitForEndOfFrame();
        }

        BlowController.gameObject.SetActive(false);

        yield return new WaitForSeconds(StateInterval);

        State = FlyingCandlesState.Undimming;

        alpha = DimmedAlpha;

        while (alpha < 1.0f)
        {
            alpha += DimVelocity * Time.deltaTime;

            foreach(var s in DimSprites)
            {
                s.color = new Color(s.color.r, s.color.g, s.color.b, alpha);
            }

            yield return new WaitForEndOfFrame();
        }

        State = FlyingCandlesState.Done;
        SecretPlatforms.SetActive(true);
    }

    public void DebugSpawnAll()
    {
        for (int i = 0; i < PendingCandles.Count; ++i)
        {
            var c = PendingCandles[i];
            c.Spawn(Vector3.zero);
            SpawnedCandles[c.ID] = c;
        }

        PendingCandles.Clear();

        StartCoroutine(lightUp());
    }
}
