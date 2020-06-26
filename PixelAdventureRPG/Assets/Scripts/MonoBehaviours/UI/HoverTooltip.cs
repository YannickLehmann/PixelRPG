using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{
    private Coroutine stopTooltip;
    bool tooltipRunning = false;
    bool corutineRunning = false;
    Vector2 mousePosition;



    private void Update()
    {
        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if ((mousePosition - new Vector2(this.transform.position.x, this.transform.position.y)).magnitude < 0.5f)
        {
            if (tooltipRunning == false) {
                StartCoroutine(startToolTip());
                    }
            if (stopTooltip != null)
            {
                StopCoroutine(stopTooltip);
            }
        }else if(tooltipRunning == true)
        {
            StartCoroutine(StopTooltip());
        }

    }


    private void OnDisable()
    {
        Tooltip.HideTooltip_Static();
    }

    private IEnumerator startToolTip()
    {
        tooltipRunning = true;
        yield return new WaitForSeconds(0.2f);

        Tooltip.HideTooltip_Static();
        Tooltip.ShowTooltip_Static(GetComponent<WeaponPickupScript>().tooltip);
    }

    private IEnumerator StopTooltip()
    {
        corutineRunning = true;
        yield return new WaitForSeconds(0.2f);
        Tooltip.HideTooltip_Static();
        corutineRunning = false;
        tooltipRunning = false;
    }

}
