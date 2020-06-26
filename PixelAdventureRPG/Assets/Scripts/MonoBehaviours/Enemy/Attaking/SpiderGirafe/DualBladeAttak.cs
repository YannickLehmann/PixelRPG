using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualBladeAttak : MonoBehaviour
{
    public float damageStrength;
    private EnemyAttak enemyAttak;
    Coroutine damageCoroutine;
    private bool attaked = false;

    private void Start()
    {
        enemyAttak = GetComponentInParent<EnemyAttak>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        enemyAttak = GetComponentInParent<EnemyAttak>();
        if (collision.gameObject.CompareTag("Player") && enemyAttak.attaking && attaked == false)
        {
            Debug.Log("DamageHim");
            Player player = collision.gameObject.GetComponent<Player>();
            attaked = true;
            StartCoroutine(DamageReset());
            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 0, this.transform.position));
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        enemyAttak = GetComponentInParent<EnemyAttak>();
        if (collision.gameObject.CompareTag("Player") && enemyAttak.attaking)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            attaked = true;
            StartCoroutine(DamageReset());
            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 0, this.transform.position));
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attaked = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamageReset()
    {
        Debug.Log("Reset");
        yield return new WaitForSeconds(0.1f);
        attaked = false;
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }
}
