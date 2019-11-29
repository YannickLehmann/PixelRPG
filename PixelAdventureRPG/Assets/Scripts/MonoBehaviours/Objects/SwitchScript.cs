using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchScript : MonoBehaviour
{
    public int currentState = 0;
    public Sprite[] sprite;
    public UnityEvent switchActivate;
    // Start is called before the first frame update



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon") && Input.GetMouseButtonDown(0))
        {
            switchActivate.Invoke();
        }
    }

    public void ChangeSwitch()
    {
        if (currentState == 0){
            currentState = 1;
            this.GetComponent<SpriteRenderer>().sprite = sprite[currentState];
        }
        else
        {
            currentState = 0;
            this.GetComponent<SpriteRenderer>().sprite = sprite[currentState];
        }
    } 
}
