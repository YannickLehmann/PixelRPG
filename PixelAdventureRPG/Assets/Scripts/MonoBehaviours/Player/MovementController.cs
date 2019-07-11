using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject weapon;
    public float movementSpeed = 3.0f;
    public GameObject spriteBody;
    Vector2 movement = new Vector2();

    public GameObject[] effects;

    [HideInInspector]
    public bool attaking = false;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public Rigidbody2D rb2D;

    private Camera c;
    private float dirextionX;
    private SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Vector3 mouse_position;

    private WeaponManager weaponManager;
    private float dodgeSpeed = 8f;
    private float dodgeCooldown = 2;
    [HideInInspector]
    public bool dodging = false;
    private bool dodgingEnabled = true;

    private void Start()
    {
        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (!dodging && Player.IsInputEnabled)
            UpdateState();

        if (Input.GetMouseButtonDown(1) && dodgingEnabled)
            DodgeMovement();
    }

    void FixedUpdate()
    {
        if(!attaking)
            WeaponRotation();

        if (!dodging && Player.IsInputEnabled)
            MoveCharacter();

        
    }

    private void DodgeMovement()
    {
        animator.SetTrigger("Dodge");
        effects[0].SetActive(false);
        effects[1].SetActive(true);
        StartCoroutine(DodgeMoving());
    }


    IEnumerator DodgeMoving()
    {
        dodging = true;
        dodgingEnabled = false;
        weaponManager.disableWeapons();

        Vector2 dodgeDirection;
        dodgeDirection.x = mouse_position.x - this.transform.position.x;
        dodgeDirection.y = mouse_position.y - this.transform.position.y;
        dodgeDirection.Normalize();

        yield return null;


        while (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDodgeAnim"))
        {
            rb2D.velocity = dodgeDirection * dodgeSpeed;

            yield return new WaitForFixedUpdate();
        }

        dodging = false;
        effects[1].SetActive(false);
        weaponManager.enableWeapons();

        yield return new WaitForSeconds(dodgeCooldown);

        dodgingEnabled = true;
    }


    public void WeaponRotation()
    {
        
        weapon.transform.rotation = Quaternion.Euler(0, 0, calculate_angle(mouse_position, this.transform.position) * Mathf.Rad2Deg);
        if (weapon.GetComponentInChildren<SpriteRenderer>())
        {
            if (dirextionX >= 0)
            {
                weapon.GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            else
            {
                weapon.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
        }
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        if (movement == Vector2.zero)
            effects[0].SetActive(false);
        else
            effects[0].SetActive(true);

        rb2D.velocity = movement * movementSpeed;
    }

    private void UpdateState()
    {
        mouse_position = c.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
        dirextionX = mouse_position.x - this.transform.position.x;
        animator.SetFloat("xDir", dirextionX);
        if (dirextionX >= 0)
        {
            spriteBody.transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipX = false; Old Rotation
        }
        else
        {
            spriteBody.transform.rotation = Quaternion.Euler(0, 180, 0);
            //spriteRenderer.flipX = true;
        }
        
    }

    private float calculate_angle(Vector3 mousePosition, Vector3 playerPosition)
    {
        float angle;
        angle = Mathf.Atan2(mousePosition.y - playerPosition.y, mousePosition.x - playerPosition.x);
        return angle;
    }
}
