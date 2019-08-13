using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveWeaponButton : MonoBehaviour
{
    public int index;
    public Image image;
    private GameObject player;
    private WeaponManager weaponManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponManager = player.GetComponent<WeaponManager>();
    }

    public void removeWeapon()
    {
        weaponManager.RemoveWeapon(index);
        this.GetComponent<Button>().enabled = false;
        this.GetComponent<Button>().enabled = true;
        if (weaponManager.weaponAmount < index)
        {
            //image.enabled = false;
        }

    }

}
