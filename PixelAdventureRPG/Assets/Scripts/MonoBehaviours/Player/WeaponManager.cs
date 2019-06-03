using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject leftArm;
    public WeaponDisplay weaponDisplay;

    private int weaponCounter = 0;
    private int weaponAmount = 0;

    private float currentCooldown;

    public  GameObject[] weapons = new GameObject[3];


    public void InitializeWeaponManager()
    {
        leftArm = GetComponent<Player>().LeftArm;
        weaponDisplay = leftArm.GetComponentInChildren<WeaponDisplay>();
        
    }

    public void DisplayWeapon(GameObject weapon)
    {
        if (weaponCounter < 3)
        {
            weapons[weaponCounter] = weapon;


            if (weaponCounter == 0)
            {
                SetUpFirstWeapon();
            }
            weaponCounter++;
            DisplayWeapons();
        }
        
    }

    public void DisplayWeapons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < weaponCounter)
            {
                weaponDisplay.weaponSprites[i].sprite = weapons[i].GetComponent<SpriteRenderer>().sprite;
                weaponDisplay.weaponSprites[i].enabled = true;
            }
            else
            {
                weaponDisplay.weaponSprites[i].enabled = false;
            }
        }
    }

    public void SetUpFirstWeapon()
    {
        weapons[0].SetActive(true);
        weapons[0].transform.localPosition = Vector3.zero;
        weapons[0].transform.localRotation = Quaternion.Euler(weapons[0].GetComponent<WeaponInterface>().weaponRaotation);
        currentCooldown = weapons[0].GetComponent<WeaponInterface>().mCooldown;
        weaponAmount = weapons[0].GetComponent<WeaponInterface>().mQuantity;
        weaponDisplay.weaponText.text = weaponAmount.ToString();

    }

    public void UseWeapon()
    {
        weaponAmount--;
        StartCoroutine(AttaktimeDisplay());


        if (weaponAmount <= 0)
        {
            if (weapons[1])
            {
                StartCoroutine(RoatateWeaponsCooldown());
            }
            else
            {
                weaponAmount = weapons[0].GetComponent<WeaponInterface>().mQuantity;
            }
        }
        weaponDisplay.weaponText.text = weaponAmount.ToString();

    }

    private void RotateWeapons()
    {
        GameObject buffer;
        if (weaponCounter > 1) {
            weapons[0].SetActive(false);
            for (int i = 0; i < weaponCounter - 1; i++)
            {
                buffer = weapons[i];
                weapons[i] = weapons[i + 1];
                weapons[i + 1] = buffer;
            }
            SetUpFirstWeapon();
        }
        DisplayWeapons();
        weaponDisplay.weaponText.text = weaponAmount.ToString();
    }

    IEnumerator CooldownDisplay()
    {
        weaponDisplay.weaponReload.enabled = true;
        weaponDisplay.weaponReload.fillAmount = 1;
        float percentComplete = 0;
        float duration = weapons[0].GetComponent<WeaponInterface>().mCooldown - weapons[0].GetComponent<WeaponInterface>().mAttackTime;

        while (percentComplete <= 1.0f)
        {
            // Time.deltaTime is the time elapsed since the last frame was drawn
            percentComplete += Time.deltaTime / duration;
            weaponDisplay.weaponReload.fillAmount = 1 - percentComplete;

            yield return null;
        }
        weaponDisplay.weaponReload.enabled = false;

    }

    IEnumerator AttaktimeDisplay()
    {
        weaponDisplay.weaponUsage.enabled = true;
        weaponDisplay.weaponUsage.fillAmount = 1;
        float percentComplete = 0;
        float duration = weapons[0].GetComponent<WeaponInterface>().mAttackTime;

        while (percentComplete <= 1.0f)
        {
            // Time.deltaTime is the time elapsed since the last frame was drawn
            percentComplete += Time.deltaTime / duration;
            weaponDisplay.weaponUsage.fillAmount = 1 - percentComplete;

            yield return null;
        }
        weaponDisplay.weaponUsage.enabled = false;

        StartCoroutine(CooldownDisplay());
    }

    private IEnumerator RoatateWeaponsCooldown()
    {
        while (weapons[0].GetComponent<WeaponInterface>().mAttaking)
            yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        RotateWeapons();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
