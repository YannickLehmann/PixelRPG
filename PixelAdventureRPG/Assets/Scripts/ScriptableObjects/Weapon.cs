using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waepon")]
public class Weapon : ScriptableObject
{
    public string weaponNamme;
    public RuntimeAnimatorController animController;
    public Sprite sprite;
    public float cooldown;
    public float attakTime;
    public float damage;
    public Vector3 rotation;

    public int quantity;
    public string tooltip;
    


}
