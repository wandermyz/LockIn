using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            var levelLink = FindObjectOfType<LevelLink>(true);
            if (levelLink != null)
            {
                SceneManager.LoadScene(levelLink.NextScene);
            }

            var animationEvent = FindObjectOfType<SwitchLevelByAnimationEvent>();
            if (animationEvent != null)
            {
                animationEvent.OnAnimationFinished();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            var m = GameObject.FindObjectOfType<FlyingCandlesManager>();
            if (m != null)
            {
                m.DebugSpawnAll();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("SceneSelection");
        }
    }
}
