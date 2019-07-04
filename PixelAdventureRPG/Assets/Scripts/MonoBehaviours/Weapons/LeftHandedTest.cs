using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandedTest : MonoBehaviour
{

    private Enemy enemy = null;
    public bool useable = true;
    public bool attaking = false;

    public MovementController movementController;
    public GameObject effectContainer;
    public Animator animator;
    public GameObject player;



    public void Start()
    {
        attaking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        movementController = player.GetComponent<MovementController>();

        animator = GetComponent<Animator>();
        effectContainer = transform.GetChild(0).gameObject;
        //Set weappon Interface values





    }

    public bool useWeapon()
    {
        if (Input.GetMouseButtonDown(1) && useable)
        {
            return true;
        }
        else
            return false;
    }


    void Update()
    {

        if (useWeapon())
        {
            StartCoroutine(attakAnimation());
        }


    }

    private IEnumerator EffectDisable()
    {
        yield return new WaitForSeconds(0.2f);
        effectContainer.SetActive(false);

    }

    public IEnumerator attakAnimation()
    {
        useable = false;
        attaking = true;
        movementController.attaking = attaking;
        effectContainer.SetActive(true);


        animator.SetBool("attack", true);

        yield return new WaitForSeconds(1);
        attaking = false;
        animator.SetBool("attack", false);
        movementController.attaking = attaking;
        StartCoroutine(EffectDisable());

        yield return new WaitForSeconds(0.5f); ;

        useable = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attaking)
        {
            if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
            {
                if (!enemy)
                {
                    enemy = collision.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageCharacter(1, 0.0f));
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we're colliding with the BoxCollider2D surrounding the Enemy,
        // Necessary since we also have a CircleCollider2D used for the Wander script,
        // and we don't care if the Ammo collides with that collider.
        if (attaking)
        {
            if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
            {
                if (!enemy)
                {
                    enemy = collision.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageCharacter(1, 0.0f));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision is BoxCollider2D) && (collision.gameObject.CompareTag("Enemy")))
        {
            enemy = null;
        }

    }
}
