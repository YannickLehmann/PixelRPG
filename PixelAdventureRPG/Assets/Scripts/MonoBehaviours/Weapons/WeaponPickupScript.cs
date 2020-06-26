using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{
    private GameObject weaponToPickUp;
    private Sprite weaponSprite;
    private bool pickupable = false;
    public string tooltip;
    public GameObject shadow;

    private void Start()
    {
        weaponToPickUp = this.gameObject.transform.GetChild(0).gameObject;
        tooltip = "DMG: " + weaponToPickUp.GetComponent<WeaponInterface>().mDamage + "\nCooldown: " + weaponToPickUp.GetComponent<WeaponInterface>().mCooldown + "\n" + weaponToPickUp.GetComponent<WeaponInterface>().mTooltip;
        weaponSprite = weaponToPickUp.GetComponent<SpriteRenderer>().sprite;
        weaponToPickUp.SetActive(false);
        this.GetComponent<SpriteRenderer>().sprite = weaponSprite;
        StartCoroutine(setPickupable());
        ShadowResize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pickupable)
        {
            pickUpWeapon(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pickupable)
        {
            pickUpWeapon(collision.gameObject);
        }
    }

    private IEnumerator setPickupable()
    {
        yield return new WaitForSeconds(0.3f);
        pickupable = true;
    }

    private void pickUpWeapon(GameObject player)
    {
        weaponToPickUp.GetComponent<WeaponInterface>().isUsed = true;
        player.GetComponent<WeaponManager>().DisplayWeapon(weaponToPickUp);
        weaponToPickUp.transform.SetParent(player.transform.GetChild(0));
        weaponToPickUp.transform.localPosition = Vector3.zero;
        weaponToPickUp.transform.localRotation = Quaternion.Euler(weaponToPickUp.GetComponent<WeaponInterface>().weaponRaotation);
        weaponToPickUp.transform.localScale = Vector3.one;
        Destroy(this.gameObject.transform.parent.gameObject); //.SetActive(false);
    }

    private void ShadowResize()
    {
        if (shadow)
        {
            shadow.transform.localScale = new Vector2(this.GetComponent<SpriteRenderer>().bounds.size.x / shadow.GetComponent<SpriteRenderer>().bounds.size.x, shadow.transform.localScale.y);
            if (this.GetComponent<SpriteRenderer>().bounds.size.y >= 0.5f)
            {
                shadow.transform.localPosition = new Vector2(shadow.transform.localPosition.x, shadow.transform.localPosition.y - 0.1f);
            }
            GetComponent<BoxCollider2D>().size = new Vector2(shadow.transform.localScale.x/2, 0.4f);
        }
    }
}
