using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Animator anim;
    private GameObject player;
    Coroutine damageCoroutine;
    public EnemyStateMashine enemyStateMashineScript;
    public List<GameObject> BloodParticles;

    float hitPoints;

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
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        InstantiateBlood();
        while (true)
        {
            enemyStateMashineScript.state = EnemyStateMashine.State.Affected;
            enemyStateMashineScript.setStatebehaviour();
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

    private void InstantiateBlood()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject blood in BloodParticles)
        {
            if (blood.activeSelf == false)
            {
                blood.transform.position = this.transform.position;
                blood.transform.rotation = Quaternion.Euler(0, 0, calculate_angle(this.transform.position, player.transform.position) * Mathf.Rad2Deg);
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