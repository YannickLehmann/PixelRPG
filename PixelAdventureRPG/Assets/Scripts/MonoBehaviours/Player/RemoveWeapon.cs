using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWeapon : MonoBehaviour
{
    private GameObject player;
    private WeaponManager weaponManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponManager = player.GetComponent<WeaponManager>();
    }

    public void removeWeapon(int index)
    {
        Debug.Log("RemoveWeapon");
        weaponManager.RemoveWeapon(index);

    }

}
