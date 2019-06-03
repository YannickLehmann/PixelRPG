using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

    public float maxHitPoints;
    public float startingHitPoints;

    public enum CharacterCategory
    {
        PLAYER,
        ENEMY
    }
    public CharacterCategory characterCategory;

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(float damage, float interval);

    public virtual IEnumerator FlickerCharacter()
    {
       
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}