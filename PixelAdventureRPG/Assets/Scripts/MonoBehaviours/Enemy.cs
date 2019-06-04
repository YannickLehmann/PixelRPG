using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    public float damageStrength;
    private Animator anim;

    Coroutine damageCoroutine;

    float hitPoints;

    private void OnEnable()
    {
        ResetCharacter();
        anim = GetComponentInChildren<Animator>();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Debug.Log("Trigger");
            anim.SetTrigger("Attaking");

            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
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