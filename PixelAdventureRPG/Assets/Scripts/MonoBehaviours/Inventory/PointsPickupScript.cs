using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickupScript : MonoBehaviour
{
    private Vector2 globalPosition;
    public Animator anim;
    public Gradient trailGradient;
    public GameObject trail;
    private float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;

    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        colorKey = new GradientColorKey[2];
        colorKey[0].color = this.GetComponent<SpriteRenderer>().color;
        colorKey[0].time = 0.0f;
        colorKey[1].color = this.GetComponent<SpriteRenderer>().color;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 0.8f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        trailGradient.SetKeys(colorKey, alphaKey);
        trail.GetComponent<TrailRenderer>().colorGradient = trailGradient;
        //trail.GetComponent<TrailRenderer>().colorGradient.alphaKeys[0].alpha = trailGradient.alphaKeys[0].alpha;
        //trail.GetComponent<TrailRenderer>().colorGradient.alphaKeys[1].alpha = trailGradient.alphaKeys[1].alpha;
        //trail.GetComponent<TrailRenderer>().colorGradient.colorKeys[0].color = this.GetComponent<SpriteRenderer>().color;
        //trail.GetComponent<TrailRenderer>().colorGradient.colorKeys[1].color = this.GetComponent<SpriteRenderer>().color;
    }

    public void Pickup(GameObject player, HealthBar healthBar)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        anim.enabled = false;

        this.transform.parent = null;
        
        StartCoroutine(MoveTorwardsPlayer(player, healthBar));

    }

    IEnumerator MoveTorwardsPlayer(GameObject player, HealthBar healthBar)
    {
        while ((player.transform.position - transform.position).magnitude >= 0.2f)
        {
            direction = (player.transform.position - transform.position).normalized;
            direction += new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f)); 
            Vector2 force = direction * speed;

            rb.AddForce(force);

            yield return new WaitForFixedUpdate();
        }

        healthBar.increasePoints(GetComponent<Consumable>().item.quantity);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
