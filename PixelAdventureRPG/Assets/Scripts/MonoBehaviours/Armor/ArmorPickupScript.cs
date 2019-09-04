using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickupScript : MonoBehaviour
{
    public enum  amorType
    {
        Helmet,
        Armor,
        Shoe
    }
    public amorType myAmorType;
    private Sprite weaponSprite;

    private void Start()
    {
        
        weaponSprite = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpArmor(collision.gameObject);
        }
    }

    private void pickUpArmor(GameObject player)
    {
        switch (myAmorType)
        {
            case amorType.Helmet:
                Debug.Log("Helmet");
                player.GetComponentInChildren<RandomPlayerColours>().setHelmet(weaponSprite, this.GetComponent<SpriteRenderer>().color);
                break;
            case amorType.Armor:
                Debug.Log("Armor");
                player.GetComponentInChildren<RandomPlayerColours>().setArmor(weaponSprite, this.GetComponent<SpriteRenderer>().color);
                break;
            case amorType.Shoe:
                Debug.Log("Shoes");
                player.GetComponentInChildren<RandomPlayerColours>().setShoes(this.GetComponent<SpriteRenderer>().color);
                break;
            default:
                break;
        }
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
