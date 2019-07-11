using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthsPrallay : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Camera.transform.position.x / 2, Camera.transform.position.y/2, 0);
    }
}
