using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteCounter : MonoBehaviour
{
    public int Value;

    public Sprite[] Sprites;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int index = (Value < Sprites.Length ? Value : Sprites.Length - 1);
        spriteRenderer.sprite = Sprites[index];        
    }
}
