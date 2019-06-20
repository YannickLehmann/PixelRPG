using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingAttak : Character
{
    public float damageStrength;
    public EnemyStateMashine enemyStateMashine;
    public EnemyAIChasing enemyAIChasing;
    private float attakCooldown = 1f;
    private bool attaking = false;
    private bool attakable = true;
    private float attakDuration = 0.5f;

    private float hitPoints = 5f;
    Coroutine damageCoroutine;

    private void Start()
    {

    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;

            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attakable && collision.gameObject.CompareTag("Player"))
        {

            enemyStateMashine.state = EnemyStateMashine.State.Attaking;
            enemyStateMashine.setStatebehaviour();
            StartCoroutine(AttakingDuration());
        }
    }

    private IEnumerator AttakingDuration()
    {
        attaking = true;
        attakable = false;
        yield return new WaitForSeconds(attakDuration);
        attaking = false;
        yield return new WaitForSeconds(attakCooldown - attakDuration);

        attakable = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attaking)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 0));
                attaking = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (enemyAIChasing.enabled)
            transform.parent.transform.rotation = Quaternion.Euler(0, 0, calculate_angle(enemyAIChasing.target.position, this.transform.position) * Mathf.Rad2Deg);
    }

    private float calculate_angle(Vector3 playerPosition, Vector3 enemyPosition)
    {
        float angle;
        angle = Mathf.Atan2(playerPosition.y - enemyPosition.y, playerPosition.x - enemyPosition.x);
        if (playerPosition.x < enemyPosition.x)
            angle += Mathf.Deg2Rad*180;
        return angle;
    }

}
