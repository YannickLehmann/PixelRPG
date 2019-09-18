using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public List<GameObject> rewards;

    private Animator anim;
    private GameObject player;
    Coroutine damageCoroutine;
    public EnemyStateMashine enemyStateMashineScript;
    private GameObject bloodParticlesContainer;
    private GameObject hitParticleContainer;
    private GameObject deathParticleContainer;
    public List<GameObject> BloodParticles;
    public List<GameObject> hitParticles;
    public List<GameObject> deathParticles;

    public Rigidbody2D rigidbody2D;
    float hitPoints;
    private CameraShake cameraShake;
    public bool effectAvailable = true;

    private void OnEnable()
    {
        ResetCharacter();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        //enemyStateMashineScript = GetComponentInParent<EnemyStateMashine>();
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
        loadBloodEffect();
        loadHitEffect();
        loadDeathEffect();
        cameraShake = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CameraShake>();
    }

    private void loadBloodEffect()
    {
        bloodParticlesContainer = GameObject.FindGameObjectWithTag("BloodParticleSystem");
        for (int i = 0; i < 20; i++)
        {
            BloodParticles.Insert(i, bloodParticlesContainer.transform.GetChild(i).gameObject);
        }
    }

    private void loadHitEffect()
    {
        hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticleSystem");
        for (int i = 0; i < 20; i++)
        {
            hitParticles.Insert(i, hitParticleContainer.transform.GetChild(i).gameObject);
        }
    }

    private void loadDeathEffect()
    {
        deathParticleContainer = GameObject.FindGameObjectWithTag("DeathParticleSystem");
        for (int i = 0; i < 10; i++)
        {
            deathParticles.Insert(i, deathParticleContainer.transform.GetChild(i).gameObject);
        }
    }

    public override IEnumerator DamageCharacter(float damage, float interval, Vector3 position)
    {
        if (effectAvailable)
        {
            effectAvailable = false;
            InstantiateBlood(position);
            InstantiateHit(position);
            StartCoroutine(EffectCooldown());
        }
        rigidbody2D.AddForce(new Vector2(transform.position.x - position.x, transform.position.y - position.y).normalized * damage* damage * 50);
        while (true)
        {
            if (enemyStateMashineScript)
            {
                enemyStateMashineScript.state = EnemyStateMashine.State.Affected;
                enemyStateMashineScript.setStatebehaviour();
            }
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;

            if (hitPoints <= float.Epsilon)
            {
                if (cameraShake)
                    cameraShake.Shake(damage * 10, damage * 5, 0.2f);
                InstantiateDeath(position);
                SpawnReward();
                KillCharacter();
                break;
            }
            cameraShake.Shake(damage * 4, damage * 2, 0.1f);

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

    private void InstantiateHit(Vector3 position)
    {
        foreach (GameObject hit in hitParticles)
        {
            if (hit.activeSelf == false)
            {
                hit.transform.position = this.transform.position;
                hit.SetActive(true);
                return;
            }
        }
    }

    private void InstantiateDeath(Vector3 position)
    {
        foreach (GameObject death in deathParticles)
        {
            if (death.activeSelf == false)
            {
                death.transform.position = this.transform.position;
                death.SetActive(true);
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

    private IEnumerator EffectCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        effectAvailable = true;
    }

    private void SpawnReward()
    {
        if (rewards.Count > 0)
        {

            float i = Random.Range(0, 10);
            if (i < 9)
            {
                GameObject deathReward = Instantiate(rewards[0]);
                deathReward.transform.position = transform.position;
            }
            else
            {
                GameObject deathReward = Instantiate(rewards[1]);
                deathReward.transform.position = transform.position;
            }
        }
    }
}