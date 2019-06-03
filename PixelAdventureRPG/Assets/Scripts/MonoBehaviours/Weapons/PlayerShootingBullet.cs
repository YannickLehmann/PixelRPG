using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingBullet : MonoBehaviour
{
    private bool collBool = false;
    private Vector3 endPos;
    private GameObject RPGManager;

    private Component[] PlayerSprites;

    private void Start()
    {
        //RPGManager = GameObject.FindGameObjectWithTag("RPGManager");
    }

    public void StartTravelBullet(Vector3 destination, float duration, GameObject player)
    {
        //player.SetActive(false);
        PlayerSprites = player.GetComponentsInChildren<Transform>();
        foreach (Transform transform in PlayerSprites)
        {
            if (transform.gameObject.CompareTag("PlayerBody"))
                transform.gameObject.SetActive(false);
        }

        if (RPGManager)
        {
            RPGManager.GetComponent<RPGGameManager>().CameraFollowGameObject(this.gameObject);
        }
        StartCoroutine(TravelBullet(destination, duration, player));
    }

    private IEnumerator TravelBullet(Vector3 destination, float duration, GameObject player)
    {
        var startPosition = transform.position;
        var percentComplete = 0.0f;

        while (percentComplete <= 1.0f && !collBool)
        {
            // Time.deltaTime is the time elapsed since the last frame was drawn
            percentComplete += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete);
            yield return null;
        }
        if (collBool)
        {
            endPos -= (destination - startPosition).normalized * 1.5f;
            collBool = false;
        }
        else
        {
            endPos = this.transform.position;
        }
        endPos.z = 0;
        player.transform.position = endPos;
        //Set Camera back to player
        //player.SetActive(true);
        foreach (Transform transform in PlayerSprites)
        {
            if (transform.gameObject.CompareTag("PlayerBody"))
                transform.gameObject.SetActive(true);
        }


        if (RPGManager)
        {
            RPGManager.GetComponent<RPGGameManager>().CameraFollowGameObject(player);
        }

        //deactivate when completet
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if a collision witch an obstacle happend
        if(collision.gameObject.layer == LayerMask.NameToLayer("Default") && (!collision.gameObject.CompareTag("Ground")))
        {
            endPos = this.transform.position;
            collBool = true;
        }
            
    }
}
