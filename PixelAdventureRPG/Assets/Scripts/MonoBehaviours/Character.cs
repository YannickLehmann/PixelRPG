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
        Transform parent = transform;


        while (parent.parent)
        {
            parent = parent.parent;
        }
        Destroy(parent.gameObject);

    }

    public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(float damage, float interval, Vector3 position);

    public virtual IEnumerator FlickerCharacter()
    {
       
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}