using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Background : MonoBehaviour
{
    public Sprite[] sprite;
    public SwitchScript switchScript;

    public void changeBackground()
    {
        this.GetComponent<SpriteRenderer>().sprite = sprite[switchScript.currentState];
    }

}
