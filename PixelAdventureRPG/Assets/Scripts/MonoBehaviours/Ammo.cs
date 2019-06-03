﻿using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float damageInflicted;
    public bool piercing = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we're colliding with the BoxCollider2D surrounding the Enemy,
        // Necessary since we also have a CircleCollider2D used for the Wander script,
        // and we don't care if the Ammo collides with that collider.
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));
            if (!piercing)
            {
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default") && (!collision.gameObject.CompareTag("Ground")) && (!piercing))
        {
            gameObject.SetActive(false);
        }
    }
}