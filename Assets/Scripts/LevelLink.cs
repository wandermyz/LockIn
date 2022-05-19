using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelLink : MonoBehaviour
{
    public string NextScene;
    public SpriteRenderer FadeOutMask;
    public Color FadeOutColor = Color.white;
    public float FadeOutSpeed = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        
        GoToNext();
    } 

    public void GoToNext()
    {
        StartCoroutine(fadeOut());
    }

    private IEnumerator fadeOut()
    {
        SpriteRenderer r = Instantiate(FadeOutMask);
        r.color = new Color(FadeOutColor.r, FadeOutColor.g, FadeOutColor.b, 0);
    
        while (r.color.a < 1)
        {
            float a = r.color.a + FadeOutSpeed * Time.deltaTime;
            r.color = new Color(FadeOutColor.r, FadeOutColor.g, FadeOutColor.b, a);
            yield return null;
        }

        SceneManager.LoadScene(NextScene);
    }
}
