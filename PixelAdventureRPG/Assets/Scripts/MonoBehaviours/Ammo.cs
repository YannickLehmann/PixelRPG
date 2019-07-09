using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float damageInflicted;
    public bool piercing = false;
    public bool enemyAmmo = false;
    public Vector3 startPos;


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we're colliding with the BoxCollider2D surrounding the Enemy,
        // Necessary since we also have a CircleCollider2D used for the Wander script,
        // and we don't care if the Ammo collides with that collider.

        if (!enemyAmmo)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f, startPos));
                if (!piercing)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();
                StartCoroutine(player.DamageCharacter(damageInflicted, 0.0f, startPos));
                if (!piercing)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocking") && (!collision.gameObject.CompareTag("Ground")) && (!piercing))
        {
            gameObject.SetActive(false);
        }
    }
}