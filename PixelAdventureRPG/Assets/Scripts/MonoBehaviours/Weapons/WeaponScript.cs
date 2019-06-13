using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    public Weapon weapon;
    [HideInInspector]
    public Animator animator;
    public bool useable = true;
    public bool attaking = false;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public MovementController movementController;
    [HideInInspector]
    public GameObject effectContainer;

    [HideInInspector]
    public WeaponInterface weaponInterface;


    // Set the Correct Animation Controller for the current weapon
    public void Start()
    {
        attaking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        movementController = player.GetComponent<MovementController>();
        
        animator = GetComponent<Animator>();
        effectContainer = transform.GetChild(0).gameObject;
        //Set weappon Interface values
        weaponInterface = this.GetComponent<WeaponInterface>();
        weaponInterface.defaultSprite = this.GetComponent<SpriteRenderer>().sprite;
        weaponInterface.setValues(weapon.attakTime, weapon.cooldown, weapon.quantity, attaking, weapon.rotation);
        weaponInterface.setWeaponManager(player.GetComponent<WeaponManager>());




        //Weapon Specific Initialazation
        Initialize();
    }

    // Chek if tha player uses this weapon
    public bool useWeapon()
    {
        if (Input.GetMouseButtonDown(0) && useable && (weaponInterface.restAmount > 0))
        {
            return true;
        }
        else
            return false;
    }

    public bool usePermaWeapon()
    {
        if (Input.GetMouseButton(0) && useable && (weaponInterface.restAmount > 0))
        {
            return true;
        }
        else
            return false;
    }

    private void OnEnable()
    {
        if (weaponInterface)
        {
            if (weaponInterface.isUsed)
            {
                
                this.GetComponent<SpriteRenderer>().sprite = weapon.sprite;
                weaponInterface.isUsed = false;
            }
        }
    }


    private void OnDisable()
    {
        attaking = false;
        useable = true;
    }

    public virtual IEnumerator attakAnimation()
    {
        useable = false;
        attaking = true;
        movementController.attaking = attaking;
        effectContainer.SetActive(true);

        weaponInterface.setValues(weapon.attakTime, weapon.cooldown, weapon.quantity, attaking, weapon.rotation);
        weaponInterface.WeaponUse();
        animator.SetBool("attack", true);

        yield return new WaitForSeconds(weapon.attakTime);
        attaking = false;
        animator.SetBool("attack", false);
        movementController.attaking = attaking;
        StartCoroutine(EffectDisable());

        yield return new WaitForSeconds(weapon.cooldown - weapon.attakTime);;

        weaponInterface.setValues(weapon.attakTime, weapon.cooldown, weapon.quantity, attaking, weapon.rotation);
        useable = true;
    }

    private IEnumerator EffectDisable()
    {
        yield return new WaitForSeconds(0.2f);
        effectContainer.SetActive(false);

    }

    public abstract void Initialize();

}
