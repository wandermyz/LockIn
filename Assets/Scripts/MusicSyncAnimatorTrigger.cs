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
    }

    void Update()
    {
        if (BGMManager.Instance.OnBeatThisFrame)
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
}
