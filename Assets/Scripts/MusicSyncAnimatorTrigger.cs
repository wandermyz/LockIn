using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MusicSyncAnimatorTrigger : MonoBehaviour
{
    public int EveryNBeats = 1;

    public string TriggerName = "MusicSync";

    [Header("Debug")]
    public int beats = -1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        BGMManager.Instance.BeatHit += OnBeatHit;
    }

    void OnDestroy()
    {
        BGMManager.Instance.BeatHit -= OnBeatHit;
    }

    void OnBeatHit(object sender, EventArgs e)
    {
        beats++;

        if (beats >= EveryNBeats)
        {
            beats = 0;
        }

        if (beats == 0)
        {
            animator.SetTrigger(TriggerName);
        }
    }
}
