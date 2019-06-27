﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingAttak : MonoBehaviour
{
    public float damageStrength;
    private EnemyAttak enemyAttak;
    Coroutine damageCoroutine;

    private void Start()
    {
        enemyAttak = GetComponentInParent<EnemyAttak>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        enemyAttak = GetComponentInParent<EnemyAttak>();
        if (collision.gameObject.CompareTag("Player") && enemyAttak.attaking)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            // only call DamageCharacter on the player if we don't currently have a DamageCharacter() Coroutine running.
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 0));
                enemyAttak.attaking = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
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
