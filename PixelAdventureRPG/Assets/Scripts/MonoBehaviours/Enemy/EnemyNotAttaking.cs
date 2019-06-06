using System.Collections;
using System.Collections;
using UnityEngine;

public class EnemyNotAttaking : Character
{


    Coroutine damageCoroutine;

    float hitPoints;

    private void OnEnable()
    {
        ResetCharacter();
        
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

   
}