using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject weapon;
    public float movementSpeed = 3.0f;
    public GameObject spriteBody;
    Vector2 movement = new Vector2();

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

    private void Start()
    {
        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        if(!attaking)
            WeaponRotation();


        MoveCharacter();
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
