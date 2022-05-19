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
                var parent = levelLink.transform.parent;
                while (parent != null)
                {
                    parent.gameObject.SetActive(true);
                    parent = parent.transform.parent;
                }
                levelLink.gameObject.SetActive(true);
                levelLink.GoToNext();
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
