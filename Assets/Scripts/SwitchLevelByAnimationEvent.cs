using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevelByAnimationEvent : MonoBehaviour
{
    public string Scene;

    public void OnAnimationFinished()
    {
        if (Scene != null)
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
