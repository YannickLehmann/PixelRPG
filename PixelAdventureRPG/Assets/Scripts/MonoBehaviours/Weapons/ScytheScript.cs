using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheScript : WeaponScript
{

    private Enemy enemy = null;


    public override void Initialize()
    {
        //nothing to do
    }

    void Update()
    {

        if (useWeapon())
        {
            StartCoroutine(attakAnimation());
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attaking)
        {
            if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
            {
                if (!enemy)
                {
                    enemy = collision.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageCharacter(weapon.damage, 0.0f, this.transform.position));
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we're colliding with the BoxCollider2D surrounding the Enemy,
        // Necessary since we also have a CircleCollider2D used for the Wander script,
        // and we don't care if the Ammo collides with that collider.
        if (attaking)
        {
            if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
            {
                if (!enemy)
                {
                    enemy = collision.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageCharacter(weapon.damage, 0.0f, this.transform.position));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
        {
            enemy = null;
        }

    }
}
