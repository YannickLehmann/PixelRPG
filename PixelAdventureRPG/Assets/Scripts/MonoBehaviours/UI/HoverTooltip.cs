using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{
    private Coroutine stopTooltip;
    bool tooltipRunning = false;

    private void OnMouseOver()
    {
        if (tooltipRunning)
        {
            StopCoroutine(stopTooltip);
        }
        Tooltip.ShowTooltip_Static(GetComponent<WeaponPickupScript>().tooltip);
    }

    private void OnMouseExit()
    {
        stopTooltip = StartCoroutine(StopTooltip());
    }

    private void OnDisable()
    {
        Tooltip.HideTooltip_Static();
    }

    private IEnumerator StopTooltip()
    {
        tooltipRunning = true;
        yield return new WaitForSeconds(0.2f);
        Tooltip.HideTooltip_Static();
        tooltipRunning = false;
    }

}
