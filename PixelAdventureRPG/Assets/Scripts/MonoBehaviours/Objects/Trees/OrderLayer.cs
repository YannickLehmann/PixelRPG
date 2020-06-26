using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayer : MonoBehaviour
{

    private float yPostion;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        yPostion = this.transform.position.y;

        spriteRenderer.sortingOrder = -1 * (int)(yPostion * 2) * 4; 
    }

}
