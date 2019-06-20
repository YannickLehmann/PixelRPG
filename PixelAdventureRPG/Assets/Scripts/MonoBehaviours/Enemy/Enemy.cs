using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    private Animator anim;
    Coroutine damageCoroutine;
    public EnemyStateMashine enemyStateMashineScript;

    float hitPoints;

    private void OnEnable()
    {
        ResetCharacter();
        anim = GetComponent<Animator>();
        //enemyStateMashineScript = GetComponentInParent<EnemyStateMashine>();
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
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
}