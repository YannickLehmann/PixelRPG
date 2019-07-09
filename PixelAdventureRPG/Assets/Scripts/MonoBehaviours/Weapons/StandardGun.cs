using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardGun : WeaponScript
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    private int poolSize = 1;

    private SpriteRenderer spriteRenderer;
    public float weaponVelocity;

    public override void Initialize()
    {
        // object pool
        poolSize = weapon.quantity;
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();


            for (int i = 0; i < poolSize; i++)
            {
                GameObject ammoObject = Instantiate(ammoPrefab);
                ammoObject.SetActive(false);
                ammoPool.Add(ammoObject);
            }
        }
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (useWeapon())
        {
            StartCoroutine(attakAnimation());
            FireAmmo();
        }
        if (!attaking)
        {
            //GetComponentInParent<Rigidbody2D>().freezeRotation = false;
        }

    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.transform.position = location;
                ammo.SetActive(true);
                ammo.GetComponent<Ammo>().startPos = this.transform.position;
                return ammo;
            }
        }
        return null;
    }

    void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);
        ammo.transform.position += Vector3.up * 0.1f;
        if (ammo != null)
        {
            StandardBullet bulletScript = ammo.GetComponent<StandardBullet>();
            Ammo ammoScript = ammo.GetComponent<Ammo>();
            ammoScript.piercing = false;
            ammoScript.damageInflicted = weapon.damage;
            bulletScript.StartTravelBullet(mousePosition, weaponVelocity);
        }

    }
}
