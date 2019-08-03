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
                Debug.Log("i: " + i + weapons[i].GetComponent<SpriteRenderer>().sprite);
                weaponDisplay.weaponSprites[i].sprite = weapons[i].GetComponent<WeaponInterface>().defaultSprite;
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
        weapons[0].GetComponent<WeaponInterface>().restAmount = weaponAmount;
        weaponDisplay.weaponText.text = weaponAmount.ToString();

    }

    public void UseWeapon()
    {
        weaponAmount--;
        weapons[0].GetComponent<WeaponInterface>().restAmount = weaponAmount;
        


        if (weaponAmount <= 0)
        {
            if (weapons[1])
            {
                StartCoroutine(RoatateWeaponsCooldown());
            }
            else
            {             
                currentCooldown = currentCooldown + 3;
                Debug.Log("LongCooldown");
                StartCoroutine(Reload());
            }
        }
        weaponDisplay.weaponText.text = weaponAmount.ToString();

        Debug.Log("CooldownDisplay");
        StartCoroutine(AttaktimeDisplay());
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
        float duration = currentCooldown - weapons[0].GetComponent<WeaponInterface>().mAttackTime;

        while (percentComplete <= 1.0f)
        {
            // Time.deltaTime is the time elapsed since the last frame was drawn
            percentComplete += Time.deltaTime / duration;
            weaponDisplay.weaponReload.fillAmount = 1 - percentComplete;

            yield return null;
        }
        weaponDisplay.weaponReload.enabled = false;

    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(currentCooldown);
        weaponAmount = weapons[0].GetComponent<WeaponInterface>().mQuantity;
        weapons[0].GetComponent<WeaponInterface>().restAmount = weaponAmount;
        SetUpFirstWeapon();
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


    public void disableWeapons()
    {

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i])
            {
                weapons[i].GetComponent<SpriteRenderer>().enabled = false;
                if (weapons[i].GetComponent<BoxCollider2D>())
                    weapons[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        if (weaponDisplay.weaponUsage.enabled == true)
            weapons[0].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void enableWeapons()
    {
         for (int i = 0; i < weapons.Length; i++)
         {
             if (weapons[i])
             {
                 weapons[i].GetComponent<SpriteRenderer>().enabled = true;
                 if (weapons[i].GetComponent<BoxCollider2D>())
                     weapons[i].GetComponent<BoxCollider2D>().enabled = true;
             }
         }
        if (weaponDisplay.weaponUsage.enabled == true)
            weapons[0].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ResetFirstWeapon()
    {
        StopAllCoroutines();
        SetUpFirstWeapon();
        weaponDisplay.weaponUsage.enabled = false;
        weaponDisplay.weaponReload.enabled = false;

    }
}
