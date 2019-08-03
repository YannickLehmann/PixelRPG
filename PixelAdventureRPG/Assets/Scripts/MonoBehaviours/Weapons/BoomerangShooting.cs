using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangShooting : MonoBehaviour
{
    public Boomerang boomerang;
    private bool catchable = false;

    public void StartTravelBoomerang(Vector3 endpos, float velocity, Boomerang localBoomerang)
    {
        boomerang = localBoomerang;
        StartCoroutine(TravelArc(endpos, velocity));
        StartCoroutine(SetCatchable());

    }

    public IEnumerator TravelArc(Vector3 destination,  float duration)
    {
        var startPosition = transform.position;
        destination.z = 0;
        startPosition.z = 0;
        var percentComplete = 0.0f;
        Vector3 rotateArround = (destination + startPosition) / 2;
        float magnitude = (rotateArround- startPosition).magnitude;
        if (magnitude < 1)
            magnitude = 1;
        if (magnitude > 1.5f)
        {
            magnitude = 1.5f;
        }
        Debug.Log(magnitude);

        while (percentComplete <= 1.0f) {
            percentComplete += Time.deltaTime / duration;
            transform.RotateAround(rotateArround, Vector3.back, 125/ (1/magnitude) * Time.deltaTime);
            rotateArround = rotateArround + ((startPosition- destination).normalized * 0.005f);
            yield return null;

                }
        catchable = false;
        gameObject.SetActive(false);
    }

    private IEnumerator SetCatchable()
    {
        yield return new WaitForSeconds(0.2f);
        catchable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && catchable)
        {
            boomerang.ResetBoomerang();
            catchable = false;
            this.gameObject.SetActive(false);
        }
    }
}
