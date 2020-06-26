using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesAttaked : MonoBehaviour
{
    private WeaponScript weaponScript;
    private GameObject hitParticleContainer;
    public List<GameObject> hitParticles;
    public float hitPoints = 15;
    public bool effectAvailable = true;
    private Animator anim;
    public GameObject[] TreeLiving;
    public GameObject TreeDead;
    private bool damageable = true;


    private void Start()
    {
        loadHitEffect();
        anim = GetComponentInParent<Animator>();
    }
    private void loadHitEffect()
    {
        hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticleSystem");
        for (int i = 0; i < 20; i++)
        {
            hitParticles.Insert(i, hitParticleContainer.transform.GetChild(i).gameObject);

        }
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            weaponScript = null;
            weaponScript = collision.GetComponent<WeaponScript>();
            if (weaponScript)
            {
                if (weaponScript.attaking && damageable)
                {
                    TreeHit();
                    hitPoints -= weaponScript.weapon.damage;
                    if (hitPoints <= 0)
                    {
                        TreeDeath();
                    }
                    damageable = false;
                    StartCoroutine(DamageCooldown());
                }
            }

        }
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        damageable = true;
    }

    private void TreeDeath()
    {
        TreeDead.SetActive(true);
        foreach(GameObject living in TreeLiving)
        {
            living.SetActive(false);
        }

    }

    private void TreeHit()
    {

        if (effectAvailable)
        {
            effectAvailable = false;
            InstantiateHit(transform.position);
            StartCoroutine(EffectCooldown());
            anim.SetTrigger("Attacked");
        }
    }


    private IEnumerator EffectCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        effectAvailable = true;
    }

    private void InstantiateHit(Vector3 position)
    {
        foreach (GameObject hit in hitParticles)
        {
            if (hit.activeSelf == false)
            {
                hit.transform.position = this.transform.position;
                hit.SetActive(true);
                return;
            }
        }
    }
}
