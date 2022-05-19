using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject[] Enables;

    void Update()
    {
        if (Input.GetButtonDown("Jump") && Enables != null)    
        {
            foreach (var go in Enables)
            {
                go.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Menu"))
        {
            SceneManager.LoadScene("SceneSelection");
        }
    }
}
