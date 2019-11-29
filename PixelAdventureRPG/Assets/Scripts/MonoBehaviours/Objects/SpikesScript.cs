using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    public float damageInflicted = 1;
    public GameObject solidCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we're colliding with the BoxCollider2D surrounding the Enemy,
        // Necessary since we also have a CircleCollider2D used for the Wander script,
        // and we don't care if the Ammo collides with that collider.
        

            if (collision.gameObject.CompareTag("Player"))
            {   
                if (collision.GetComponent<MovementController>().dodging)
            {
                StartCoroutine(ActivateSolid());
            }
            else
            {
                Player player = collision.gameObject.GetComponent<Player>();
                StartCoroutine(player.DamageCharacter(damageInflicted, 0.0f, this.transform.position));
            }
                

                
            }
    }


    private IEnumerator ActivateSolid()
    {
        solidCollider.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        solidCollider.SetActive(true);
    }

 
}
