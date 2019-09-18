using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject WeaponContainer;

    private void OnDisable()
    {
        GameObject weapon = Instantiate(Weapon);
        GameObject weaponContainer = Instantiate(WeaponContainer);
        weapon.transform.SetParent(weaponContainer.transform.GetChild(0).transform);
        weaponContainer.transform.position = this.transform.position;
        weaponContainer.SetActive(true);
    }

}
