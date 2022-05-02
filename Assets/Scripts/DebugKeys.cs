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
        }
    }
}
