using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{

    private void OnMouseOver()
    {
        Tooltip.ShowTooltip_Static(GetComponent<WeaponPickupScript>().tooltip);
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
    }

    private void OnDisable()
    {
        Tooltip.HideTooltip_Static();
    }

}
