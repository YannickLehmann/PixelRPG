using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public SpawnPoint playerSpawnPoint;

    public static RPGGameManager sharedInstance = null;
    public RPGCameraManager cameraManager;

    private GameObject player;

    void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            // We only ever want one instance to exist, so destroy the other existing object
            Destroy(gameObject);
        }
        else
        {
            // If this is the only instance, then assign the sharedInstance variable to the current object.
            sharedInstance = this;
        }
    }

    void Start()
    {
        // Consolidate all the logic to setup a scene inside a single method. 
        // This makes it easier to call again in the future, in places other than the Start() method.
        SetupScene();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            player = playerSpawnPoint.SpawnObject();
            CameraFollowGameObject(player);
        }
    }

    public void CameraFollowGameObject(GameObject toFollow)
    {
        cameraManager.virtualCamera.Follow = toFollow.transform;
    }

}
