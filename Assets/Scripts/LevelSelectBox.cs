using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBox : MonoBehaviour
{
    public string Level;
    public Material PlayerMaterial;
    public Material DefaultMaterial;

    [Header("Debug")]
    public bool InThisLevel;

    private Material originalMaterial;

    void Update()
    {
        if (Input.GetButtonDown("Menu") && InThisLevel && Level != null)
        {
            SceneManager.LoadScene(Level);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        InThisLevel = true;
        if (PlayerMaterial != null)
        {
            originalMaterial = GameManager.Instance.PlayerController.GetComponent<SpriteRenderer>().material;
            GameManager.Instance.PlayerController.GetComponent<SpriteRenderer>().material = PlayerMaterial;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        InThisLevel = false;
        if (PlayerMaterial != null)
        {
            GameManager.Instance.PlayerController.GetComponent<SpriteRenderer>().material = DefaultMaterial;
        }
    }
}
