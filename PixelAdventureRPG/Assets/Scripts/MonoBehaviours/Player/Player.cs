using UnityEngine;
using System.Collections;

public class Player : Character
{
    public HitPoints hitPoints;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    public GameObject LeftArmPrefab;
    [HideInInspector]
    public GameObject LeftArm;
    Inventory inventory;

    private MovementController movementController;

    public void Start()
	{
        ResetCharacter();
    }



    public override void ResetCharacter()
    {
        LeftArm = Instantiate(LeftArmPrefab);
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;

        hitPoints.value = startingHitPoints;

        GetComponent<WeaponManager>().InitializeWeaponManager();
        movementController = GetComponent<MovementController>();
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

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        if (!movementController.dodging)
        {
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