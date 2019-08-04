﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Animator anim;
    private GameObject player;
    Coroutine damageCoroutine;
    public EnemyStateMashine enemyStateMashineScript;
    private GameObject bloodParticlesContainer;
    public List<GameObject> BloodParticles;

    public Rigidbody2D rigidbody2D;
    float hitPoints;
    private CameraShake cameraShake;

    private void OnEnable()
    {
        ResetCharacter();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        //enemyStateMashineScript = GetComponentInParent<EnemyStateMashine>();
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
        loadBloodEffect();
        cameraShake = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CameraShake>();
    }

    private void loadBloodEffect()
    {
        bloodParticlesContainer = GameObject.FindGameObjectWithTag("BloodParticleSystem");
        for (int i = 0; i < 20; i++)
        {
            BloodParticles.Insert(i, bloodParticlesContainer.transform.GetChild(i).gameObject);
        }
    }

    public override IEnumerator DamageCharacter(float damage, float interval, Vector3 position)
    {
        InstantiateBlood(position);
        rigidbody2D.AddForce(new Vector2(transform.position.x - position.x, transform.position.y - position.y).normalized * damage* damage * 50);
        while (true)
        {
            enemyStateMashineScript.state = EnemyStateMashine.State.Affected;
            enemyStateMashineScript.setStatebehaviour();
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;

            if (hitPoints <= float.Epsilon)
            {
                if (cameraShake)
                    cameraShake.Shake(damage * 10, damage * 5, 0.2f);
                KillCharacter();
                break;
            }
            cameraShake.Shake(damage * 4, damage * 2, 0.1f);

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

    private void InstantiateBlood(Vector3 position)
    {

        foreach (GameObject blood in BloodParticles)
        {
            if (blood.activeSelf == false)
            {
                blood.transform.position = this.transform.position;
                blood.transform.rotation = Quaternion.Euler(0, 0, calculate_angle(this.transform.position, position) * Mathf.Rad2Deg);
                blood.SetActive(true);
                return;
            }
        }
    }

    private float calculate_angle(Vector3 mousePosition, Vector3 playerPosition)
    {
        float angle;
        angle = Mathf.Atan2(mousePosition.y - playerPosition.y, mousePosition.x - playerPosition.x);
        return angle;
    }
}