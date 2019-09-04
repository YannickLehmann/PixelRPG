using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInterface : MonoBehaviour
{
    [HideInInspector]
    public float mAttackTime;
    [HideInInspector]
    public float mCooldown;
    [HideInInspector]
    public int mQuantity;
    [HideInInspector]
    public bool mAttaking;
    [HideInInspector]
    public bool isUsed = false;
    [HideInInspector]
    public Vector3 weaponRaotation;
    [HideInInspector]
    public Sprite defaultSprite;
    [HideInInspector]
    public int restAmount;
    [HideInInspector]
    public string mTooltip = "empty";
    [HideInInspector]
    public float mDamage;

    private WeaponManager weaponManager;

    public void setValues(float attakTime,float cooldown, int quantity, bool attaking, Vector3 rotation, string tooltip, float Damage)
    {
        mAttackTime = attakTime;
        mCooldown = cooldown;
        mQuantity = quantity;
        mAttaking = attaking;
        weaponRaotation = rotation;
        mTooltip = tooltip;
        mDamage = Damage;
    }



    public void setWeaponManager(WeaponManager manager)
    {
        weaponManager = manager;
    }

    public void WeaponUse()
    {
        weaponManager.UseWeapon();
    }

    public void ResetWeapon()
    {
        weaponManager.ResetFirstWeapon();
    }
}
