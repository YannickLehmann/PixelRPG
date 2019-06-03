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
        player = GameObject.FindGameObjectWithTag("Player");
        movementController = player.GetComponent<MovementController>();
        weaponInterface = this.GetComponent<WeaponInterface>();
        animator = GetComponent<Animator>();
        attaking = false;
        weaponInterface.setValues(weapon.attakTime, weapon.cooldown, weapon.quantity, attaking, weapon.rotation);
        weaponInterface.setWeaponManager(player.GetComponent<WeaponManager>());
        effectContainer = transform.GetChild(0).gameObject;


        //Weapon Specific Initialazation
        Initialize();
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
        Debug.Log("Attaking");
        yield return new WaitForSeconds(weapon.attakTime);
        attaking = false;
        animator.SetBool("attack", false);
        movementController.attaking = attaking;
        StartCoroutine(EffectDisable());

        Debug.Log("Recharging");
        yield return new WaitForSeconds(weapon.cooldown - weapon.attakTime);
        Debug.Log("Ready");

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
