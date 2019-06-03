using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{
    private GameObject weaponToPickUp;
    private Sprite weaponSprite;

    private void Start()
    {
        weaponToPickUp = this.gameObject.transform.GetChild(0).gameObject;
        weaponSprite = weaponToPickUp.GetComponent<SpriteRenderer>().sprite;
        weaponToPickUp.SetActive(false);
        this.GetComponent<SpriteRenderer>().sprite = weaponSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpWeapon(collision.gameObject);
        }
    }

    private void pickUpWeapon(GameObject player)
    {
        weaponToPickUp.GetComponent<WeaponInterface>().isUsed = true;
        player.GetComponent<WeaponManager>().DisplayWeapon(weaponToPickUp);
        weaponToPickUp.transform.SetParent(player.transform.GetChild(0));
        weaponToPickUp.transform.localPosition = Vector3.zero;
        weaponToPickUp.transform.localRotation = Quaternion.Euler(weaponToPickUp.GetComponent<WeaponInterface>().weaponRaotation);
        this.gameObject.SetActive(false);
    }
}
