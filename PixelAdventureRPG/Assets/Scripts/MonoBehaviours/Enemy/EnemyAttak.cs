using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttak : MonoBehaviour
{

    public EnemyStateMashine enemyStateMashine;
    public EnemyAIChasing enemyAIChasing;
    public float attakCooldown = 3f;
    public bool attaking = false;
    private bool attakable = true;
    public float attakDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attakable && collision.gameObject.CompareTag("Player"))
        {

            enemyStateMashine.state = EnemyStateMashine.State.Attaking;
            enemyStateMashine.setStatebehaviour();
            StartCoroutine(AttakingDuration());
        }
    }

    private IEnumerator AttakingDuration()
    {
        attaking = true;
        attakable = false;
        yield return new WaitForSeconds(attakDuration);
        attaking = false;
        yield return new WaitForSeconds(attakCooldown - attakDuration);

        attakable = true;
    }

    private void FixedUpdate()
    {
        if (enemyAIChasing.enabled)
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
}
