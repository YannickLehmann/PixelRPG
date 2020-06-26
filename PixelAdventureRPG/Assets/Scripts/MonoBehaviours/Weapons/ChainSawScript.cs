using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSawScript : WeaponScript
{
    private Enemy enemy = null;
    private bool damageable = true;


    public override void Initialize()
    {
        //nothing to do
    }

    void Update()
    {

        if (useWeapon())
        {
            StartCoroutine(attakAnimation());
            StartCoroutine(resetMovementControll());
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (attaking)
        {
            if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
            {
                if (true)
                {
                    enemy = collision.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageCharacter(weapon.damage, 0.0f, this.transform.position));
                    damageable = false;
                    StartCoroutine(DamageCooldown());
                }
            }
        }

    }

    private IEnumerator resetMovementControll()
    {
        yield return new WaitForFixedUpdate();
        movementController.attaking = false;
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        damageable = true;
    }

}
