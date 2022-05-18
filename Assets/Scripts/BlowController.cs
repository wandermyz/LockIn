using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowController : MonoBehaviour
{
    public List<FlyingCandle> Candles {get; set;}
    public float BreathMinVelocity;
    public float BlowMinVelocity;
    public float AirInVelocity;
    public float PositiveLerpRatio;
    public float NegativeLerpRatio;

    [Header("Debug")]
    public bool IsDone;
    public float AirAmount;
    public float BreathLAxis;
    public float BreathRAxis;
    public float NewBreathLAxis;
    public float NewBreathRAxis;
    public float PositiveTarget;
    public float NegativeTarget;

    private Transform holder;

    void Start()
    {
        holder = transform.GetChild(0);
    }

    void Update()
    {
        NewBreathLAxis = Input.GetAxis("BreathL");
        NewBreathRAxis = Input.GetAxis("BreathR");

        float prevScale = holder.transform.localScale.y;
        float scale;
        if (NegativeTarget < 0)
        {
            scale = Mathf.Lerp(prevScale, NegativeTarget, NegativeLerpRatio * Time.deltaTime);
            if (Mathf.Abs(scale - NegativeTarget) < 0.001f)
            {
                NegativeTarget = 0;
            }
        }
        else
        {
            scale = Mathf.Lerp(prevScale, PositiveTarget, PositiveLerpRatio * Time.deltaTime);
        }
        holder.transform.localScale = new Vector3(1, scale, 1);

    }

    void FixedUpdate()
    {
        float leftVel = (NewBreathLAxis - BreathLAxis) / Time.fixedDeltaTime;
        float rightVel = (NewBreathRAxis - BreathRAxis) / Time.fixedDeltaTime;

        if (leftVel > BreathMinVelocity)
        {
            AirAmount += AirInVelocity * Time.fixedDeltaTime;
        }
        else if (leftVel < -BreathMinVelocity)
        {
            AirAmount -= AirInVelocity * Time.fixedDeltaTime;
        }
        if (rightVel > BreathMinVelocity)
        {
            AirAmount += AirInVelocity * Time.fixedDeltaTime;
        }
        else if (rightVel < -BreathMinVelocity)
        {
            AirAmount -= AirInVelocity * Time.fixedDeltaTime;
        }
        AirAmount = Mathf.Max(0, AirAmount);

        float outAmount = 0;
        if (BreathLAxis > 0.999f && leftVel < -BlowMinVelocity)
        {
            outAmount += AirAmount / 2.0f;
        }
        if (BreathRAxis > 0.999f && rightVel < -BlowMinVelocity)
        {
            outAmount += AirAmount / 2.0f;
        }
        if (outAmount > 0.01)
        {
            Debug.Log("Air Out! " + outAmount);
            NegativeTarget = -outAmount;
            AirAmount = 0;
        }

        if (NewBreathLAxis < -0.999f || NewBreathRAxis < -0.999f)
        {
            AirAmount = 0;
        }

        BreathLAxis = NewBreathLAxis;
        BreathRAxis = NewBreathRAxis;

        PositiveTarget = AirAmount;
    }
}
