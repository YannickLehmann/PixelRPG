using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int offset = 0;
    public void SetLayer (int layer)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = layer + offset;
    }
}
