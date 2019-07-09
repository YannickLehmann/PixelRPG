using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    private bool collBool = false;
    public GameObject Effect;

    public void StartTravelBullet(Vector3 destination, float duration)
    {
        if (Effect)
        {
            Effect.SetActive(false);
            StartCoroutine(startEffect());
        }
        StartCoroutine(TravelBullet(destination, duration));
    }

    private IEnumerator TravelBullet(Vector3 destination, float duration)
    {
        var startPosition = transform.position;
        var percentComplete = 0.0f;

        destination.z = 0;
        startPosition.z = 0;
        destination = (destination - startPosition).normalized;

        while (percentComplete <= 1.0f && !collBool)
        {
            // Time.deltaTime is the time elapsed since the last frame was drawn
            percentComplete += Time.deltaTime / duration;
            transform.Translate(destination * Time.deltaTime *10, Space.World);
            yield return null;
        }

        //deactivate when completet
        gameObject.SetActive(false);
    }

    IEnumerator startEffect()
    {
        yield return new WaitForSeconds(0.03f);
        Effect.SetActive(true);
    }

}
