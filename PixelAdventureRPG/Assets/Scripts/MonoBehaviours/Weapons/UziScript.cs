﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziScript : WeaponScript
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    private int poolSize = 1;
    private int quantity;

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
        quantity = weapon.quantity;
    }

    void Update()
    {

        if (Input.GetMouseButton(0) && useable && (quantity > 0))
        {
            StartCoroutine(attakAnimation());
            FireAmmo();
            quantity--;
        }else if (quantity <= 0)
        {
            StartCoroutine(Relaod());
        }
        if (!attaking)
        {
            //GetComponentInParent<Rigidbody2D>().freezeRotation = false;
        }

    }
    
    private IEnumerator Relaod()
    {
        yield return new WaitForSeconds(2);
        quantity = weapon.quantity;
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.transform.position = location;
                ammo.SetActive(true);
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