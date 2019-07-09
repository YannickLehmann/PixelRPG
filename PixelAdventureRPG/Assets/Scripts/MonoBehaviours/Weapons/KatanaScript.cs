using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaScript : WeaponScript
{
    public float dashSpeed = 4f;
    private Rigidbody2D rb2D;
    private Enemy enemy = null;
    private bool dashing = true;
    private float dashTime = 0.25f;


    public override void Initialize()
    {
        rb2D = player.GetComponent<Rigidbody2D>();
        attaking = false;
        animator.SetFloat("animationTime", weapon.attakTime);
    }

    void Update()
    {

        if (useWeapon())
        {
            DeactivateMovement();
            StartCoroutine(attakAnimation());
        }

    }

    private void DeactivateMovement()
    {
        StartCoroutine(StabMovement((movementController.mouse_position - player.transform.position)));
        movementController.animator.SetBool("isWalking", false);
        movementController.rb2D.velocity = Vector3.zero;
        movementController.enabled = false;
    }

    IEnumerator StabMovement(Vector3 direction)
    {
        dashing = true;
        Vector2 stabDirection;
        stabDirection.x = direction.x;
        stabDirection.y = direction.y;
        stabDirection.Normalize();

        yield return null;

        StartCoroutine(DashingTimer());
        while (dashing)
        {
            MoveCharacter(stabDirection);

            yield return new WaitForFixedUpdate();
        }
        movementController.enabled = true;

    }

    private IEnumerator DashingTimer()
    {
        yield return new WaitForSeconds(dashTime);
        dashing = false;
    }

    private void MoveCharacter(Vector2 direction)
    {


        //movement.Normalize();
        rb2D.velocity = direction * dashSpeed;
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
