using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleFlyAttakingScript : MonoBehaviour
{
    public float damageStrength = 1f;

    private EnemyStateMashine enemyStateMashineScript;
    private Animator anim;
    private bool attakable = true;
    private bool onlyHitOnce = true;

    Coroutine damageCoroutine;

    private void Start()
    {
        enemyStateMashineScript = GetComponentInParent<EnemyStateMashine>();
        anim = GetComponent<Animator>();
    }

    IEnumerator AttakCooldown()
    {
        attakable = false;
        yield return new WaitForSeconds(enemyStateMashineScript.attakCooldown);
        attakable = true;
        onlyHitOnce = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && attakable)
        {
            StartCoroutine(AttakCooldown());
            if (collision.transform.position.y >= transform.parent.position.y)
            {
                anim.SetBool("PlayerUp", true);
            }
            else
            {
                anim.SetBool("PlayerUp", false);
            }
            enemyStateMashineScript.state = EnemyStateMashine.State.Attaking;
            enemyStateMashineScript.setStatebehaviour();
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attakable)
        {
            StartCoroutine(AttakCooldown());
            if (collision.transform.position.y >= transform.parent.position.y)
            {
                anim.SetBool("PlayerUp", true);
            }
            else
            {
                anim.SetBool("PlayerUp", false);
            }
            enemyStateMashineScript.state = EnemyStateMashine.State.Attaking;
            enemyStateMashineScript.setStatebehaviour();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (enemyStateMashineScript.state == EnemyStateMashine.State.Attaking) && (onlyHitOnce))
        {
            onlyHitOnce = false;
            Player player = collision.gameObject.GetComponent<Player>();

            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 0, this.transform.position));

            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (enemyStateMashineScript.state == EnemyStateMashine.State.Attaking) && (onlyHitOnce))
        {
            onlyHitOnce = false;
            Player player = collision.gameObject.GetComponent<Player>();

            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 0, this.transform.position));

            }
        }
    }
}
