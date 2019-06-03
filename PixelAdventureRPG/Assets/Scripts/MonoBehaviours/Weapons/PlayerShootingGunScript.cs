using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingGunScript : WeaponScript
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    private int poolSize = 1;

    private SpriteRenderer spriteRenderer;
    public float weaponVelocity;

    public override void Initialize()
    {
        // object pool
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

        if (Input.GetMouseButtonDown(0) && useable)
        {
            StartCoroutine(attakAnimation());
            FireAmmo();
            spriteRenderer.enabled = false;
        }
        if (!attaking)
        {
            spriteRenderer.enabled = true;
            //GetComponentInParent<Rigidbody2D>().freezeRotation = false;
        }

    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);     
        if (ammo != null)
        {
            PlayerShootingBullet arcScript = ammo.GetComponent<PlayerShootingBullet>();
            Ammo ammoScript = ammo.GetComponent<Ammo>();
            ammoScript.piercing = true;
            ammoScript.damageInflicted = weapon.damage;
            float travelDuration = 1.0f / weaponVelocity;
            arcScript.StartTravelBullet(mousePosition, travelDuration, player);
        }

    }
}
