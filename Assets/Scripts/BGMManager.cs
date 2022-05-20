using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AkAmbient))]
public class BGMManager : Singleton<BGMManager>
{
    private AkAmbient akAmbient;

    public event EventHandler BeatHit;
    public event EventHandler BarHit;
    public event EventHandler<CueEventArgs> CueHit;

    public class CueEventArgs : EventArgs
    {
        public string Marker { get; set; }
    }

    public void PostTrigger(string trigger)
    {
        AkSoundEngine.PostTrigger(trigger, gameObject);
    }
    
    public void SetRTPCValue(AK.Wwise.RTPC rtpc, float value)
    {
        rtpc.SetValue(gameObject, value);
    }

    void Start()
    {
        Debug.Log("BGM Manager started: " + gameObject.GetHashCode());
        akAmbient = GetComponent<AkAmbient>();
    }
    
    void OnBeat(AkEventCallbackMsg msg)
    {
        if (BeatHit != null)
        {
            BeatHit(this, new EventArgs());
        }
    }

    void OnBar(AkEventCallbackMsg msg)
    {
        if (BarHit != null)
        {
            BarHit(this, new EventArgs());
        }
    }

    void OnCue(AkEventCallbackMsg msg)
    {
        var info = msg.info as AkMarkerCallbackInfo;
        string marker = null;
        if (info != null)
        {
            marker = info.strLabel;
        }
        CueHit(this, new CueEventArgs(){ Marker = marker });
    }
}
