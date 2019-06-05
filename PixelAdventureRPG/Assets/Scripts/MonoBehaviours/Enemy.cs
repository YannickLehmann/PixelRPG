using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    public float damageStrength;
    private Animator anim;
    private float attakCooldown = 1f;
    private bool attaking = false;
    private bool attakable = true;
    private float attakDuration = 0.5f;

    Coroutine damageCoroutine;

    float hitPoints;

    private void OnEnable()
    {
        ResetCharacter();
        anim = GetComponent<Animator>();
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
            if (collision.transform.position.y > this.transform.position.y)
            {
                anim.SetBool("PlayerUp", true);
            }
            else
            {
                anim.SetBool("PlayerUp", false);
            }
            anim.SetTrigger("Attaking");
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
        if(collision.gameObject.CompareTag("Player") && attaking)
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
}