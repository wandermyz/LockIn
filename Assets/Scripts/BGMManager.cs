using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AkAmbient))]
public class BGMManager : Singleton<BGMManager>
{
    public bool OnBeatThisFrame {get; private set;}
    public bool OnBarThisFrame {get; private set;}
    public string CueThisFrame {get; private set;}
    private AkAmbient akAmbient;
    void Start()
    {
        akAmbient = GetComponent<AkAmbient>();
    }
    
    void Update()
    {

    }

    void LateUpdate()
    {
        OnBeatThisFrame = false;
        OnBarThisFrame = false;
        CueThisFrame = null;
    }

    void OnBeat(AkEventCallbackMsg msg)
    {
        OnBeatThisFrame = true;
    }

    void OnBar(AkEventCallbackMsg msg)
    {
        OnBarThisFrame = true;
    }

    void OnCue(AkEventCallbackMsg msg)
    {
        CueThisFrame = "todo";
    }
}
