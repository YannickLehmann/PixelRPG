using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponedRangedEnemyScript : MonoBehaviour
{
    public EnemyStateMashine enemyStateMashine;
    public EnemyAIChasing enemyAIChasing;
    public float attakCooldown = 3f;
    public bool attaking = false;
    private bool attakable = true;
    private float attakDuration = 0.2f;
    private bool aiming = false;

    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    private int poolSize = 5;

    public float weaponVelocity;


    // Start is called before the first frame update
    void Start()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();


            for (int i = 0; i < poolSize; i++)
            {
                GameObject ammoObject = Instantiate(ammoPrefab);
                ammoObject.SetActive(false);
                ammoPool.Add(ammoObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            aiming = true;
            enemyStateMashine.state = EnemyStateMashine.State.Idle;
            enemyStateMashine.setStatebehaviour();

            if (attakable)
            {

                enemyStateMashine.state = EnemyStateMashine.State.Attaking;
                enemyStateMashine.setStatebehaviour();
                StartCoroutine(AttakingDuration(collision.transform.position));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        aiming = false;
        enemyStateMashine.state = EnemyStateMashine.State.Chasing;
        enemyStateMashine.setStatebehaviour();
    }

    private IEnumerator AttakingDuration(Vector3 target)
    {
        attaking = true;
        attakable = false;
        yield return new WaitForSeconds(attakDuration);
        FireAmmo(target);
        Debug.Log("TriggerIdle");
        enemyStateMashine.state = EnemyStateMashine.State.Idle;
        enemyStateMashine.setStatebehaviour();
        attaking = false;
        yield return new WaitForSeconds(attakCooldown - attakDuration);

        attakable = true;
    }

    private void FixedUpdate()
    {
        if (aiming)
            transform.rotation = Quaternion.Euler(0, 0, calculate_angle(enemyAIChasing.target.position, this.transform.position) * Mathf.Rad2Deg);
    }

    private float calculate_angle(Vector3 playerPosition, Vector3 enemyPosition)
    {
        float angle;
        angle = Mathf.Atan2(playerPosition.y - enemyPosition.y, playerPosition.x - enemyPosition.x);
        if (playerPosition.x < enemyPosition.x)
            angle += Mathf.Deg2Rad * 180;
        return angle;
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.transform.position = location;
                ammo.SetActive(true);
                ammo.GetComponent<Ammo>().startPos = this.transform.position;
                return ammo;
            }
        }
        return null;
    }

    void FireAmmo(Vector3 target)
    {
        GameObject ammo = SpawnAmmo(transform.position);
        
        if (ammo != null)
        {
            ammo.transform.rotation = Quaternion.Euler(0, 0, calculate_angle(enemyAIChasing.target.position, this.transform.position) * Mathf.Rad2Deg);
            ammo.transform.position += Vector3.up * 0.1f;
            StandardBullet bulletScript = ammo.GetComponent<StandardBullet>();
            Ammo ammoScript = ammo.GetComponent<Ammo>();
            ammoScript.piercing = false;
            ammoScript.damageInflicted = 1;
            bulletScript.StartTravelBullet(target, weaponVelocity);
        }

    }
}
