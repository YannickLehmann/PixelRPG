﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerColours : MonoBehaviour
{
    public SpriteRenderer[] PlayerSprites;
    // Start is called before the first frame update
    void Start()
    {
        changeColor();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            changeColor();
        }
    }

    private void changeColor()
    {
        for (int i = 0; i < PlayerSprites.Length; i++)
        {
            PlayerSprites[i].color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
}
