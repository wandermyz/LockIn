using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(load());
    }

    private IEnumerator load()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Intro");
    }
}
