using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTrigger : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            anim.SetTrigger("Collision");
    }
}
