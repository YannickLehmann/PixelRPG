using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ButtonTooltip : MonoBehaviour
{
    private GameObject player;
    private WeaponManager weaponManager;
    public string tooltip;
    private Coroutine stopTooltip;
    private bool tooltipRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponManager = player.GetComponent<WeaponManager>();
    }

    public void ShowTooltip(int i)
    {
        if (weaponManager.weapons[i])
        {
            if (tooltipRunning)
            {
                StopCoroutine(stopTooltip);
            }
            tooltip = "DMG: " + weaponManager.weapons[i].GetComponent<WeaponInterface>().mDamage + "\nCooldown: " + weaponManager.weapons[i].GetComponent<WeaponInterface>().mCooldown + "\n" + weaponManager.weapons[i].GetComponent<WeaponInterface>().mTooltip;
            Tooltip.ShowTooltip_Static(tooltip);
        }
        
    }

    public void DisableTooltip(int i)
    {
        stopTooltip = StartCoroutine(StopTooltip());
    }

    private IEnumerator StopTooltip()
    {
        tooltipRunning = true;
        yield return new WaitForSeconds(0.2f);
        Tooltip.HideTooltip_Static();
        tooltipRunning = false;
    }

}
