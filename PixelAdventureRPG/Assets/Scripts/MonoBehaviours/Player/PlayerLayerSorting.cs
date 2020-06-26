using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerSorting : MonoBehaviour
{
    public float offsetY;
    private float yPostion;

    private int sortingLayer;
    private PartLayer[] partLayers;
    // Start is called before the first frame update
    private void findSortingLayer()
    {
        yPostion = this.transform.position.y + offsetY;

        sortingLayer = -1 * (int)(yPostion * 2) * 4;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        findSortingLayer();
        partLayers = GetComponentsInChildren<PartLayer>();
        
        foreach(PartLayer partlayer in partLayers)
        {
            partlayer.SetLayer(sortingLayer);
        }

    }
}
