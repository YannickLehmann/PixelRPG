using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : WeaponScript
{
    public GameObject boomerang;
    private GameObject boomerangObject;

    private SpriteRenderer spriteRenderer;
    public float weaponVelocity;

    private Coroutine boomerangCoroutine;


    public override void Initialize()
    {
        boomerangObject = Instantiate(boomerang);
        boomerangObject.SetActive(false);
    }

    void Update()
    {

        if (useWeapon())
        {
            boomerangCoroutine = StartCoroutine(attakAnimation());
            FireAmmo();
        }
        if (!useable)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            //GetComponentInParent<Rigidbody2D>().freezeRotation = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        boomerangObject.transform.position = transform.position;
        boomerangObject.SetActive(true);
        boomerangObject.transform.position += Vector3.up * 0.1f;
        if (boomerangObject != null)
        {
            BoomerangShooting boomerangScript = boomerangObject.GetComponent<BoomerangShooting>();
            boomerangObject.GetComponent<Ammo>().damageInflicted = weapon.damage;
            boomerangScript.StartTravelBoomerang(mousePosition, weapon.cooldown, this);
        }

    }

    public void ResetBoomerang()
    {
        StopCoroutine(boomerangCoroutine);
        ResetWeapon();

    }
}
