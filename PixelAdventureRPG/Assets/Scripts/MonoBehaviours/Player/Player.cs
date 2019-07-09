using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : Character
{
    public static bool IsInputEnabled = true;
    private CameraShake cameraShake;

    public HitPoints hitPoints;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    public GameObject LeftArmPrefab;
    [HideInInspector]
    public GameObject LeftArm;
    Inventory inventory;

    private Animator anim;
    private MovementController movementController;

    private GameObject bloodParticlesContainer;
    public List<GameObject> BloodParticles;

    public void Start()
	{
        ResetCharacter();
        loadBloodEffect();
    }

    private void loadBloodEffect()
    {
        bloodParticlesContainer = GameObject.FindGameObjectWithTag("BloodParticleSystem");
        for (int i = 0; i < 10; i++)
        {
            BloodParticles.Insert(i, bloodParticlesContainer.transform.GetChild(i).gameObject);
        }
    }

    public override void ResetCharacter()
    {
        LeftArm = Instantiate(LeftArmPrefab);
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;

        hitPoints.value = startingHitPoints;

        cameraShake = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CameraShake>();
        GetComponent<WeaponManager>().InitializeWeaponManager();
        movementController = GetComponent<MovementController>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;
                print("Hit: " + hitObject.objectName);

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }

                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public override IEnumerator DamageCharacter(float damage, float interval, Vector3 position)
    {
        if (!movementController.dodging)
        {
            InstantiateBlood(position);
            StartCoroutine(PlayerAffection());
            cameraShake.Shake(damage*6, damage*3, 0.2f);
            while (true)
        {
            
                StartCoroutine(FlickerCharacter());
                hitPoints.value = hitPoints.value - damage;
                if (hitPoints.value <= float.Epsilon)
                {
                    KillCharacter();
                    break;
                }
                if (interval > float.Epsilon)
                {
                    yield return new WaitForSeconds(interval);
                }
                else
                {
                    break;
                }
            }
        }
        yield return new WaitForFixedUpdate();
    }

    private void InstantiateBlood(Vector3 position)
    {

        foreach (GameObject blood in BloodParticles)
        {
            if (blood.activeSelf == false)
            {
                blood.transform.position = this.transform.position;
                blood.transform.rotation = Quaternion.Euler(0, 0, calculate_angle(this.transform.position, position) * Mathf.Rad2Deg);
                blood.SetActive(true);
                return;
            }
        }
    }

    private float calculate_angle(Vector3 mousePosition, Vector3 playerPosition)
    {
        float angle;
        angle = Mathf.Atan2(mousePosition.y - playerPosition.y, mousePosition.x - playerPosition.x);
        return angle;
    }

    private IEnumerator PlayerAffection()
    {
        anim.SetTrigger("Damaged");
        IsInputEnabled = false;
        Debug.Log("Affected");
        yield return new WaitForFixedUpdate();

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAffected"))
        {


            yield return new WaitForFixedUpdate();
        }
        IsInputEnabled = true;
        Debug.Log("Affection Over");
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
            print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
            return true;
        }
        else
        {
            return false;
        }
        
    }
}