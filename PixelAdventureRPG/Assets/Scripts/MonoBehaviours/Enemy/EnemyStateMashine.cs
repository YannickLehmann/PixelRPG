using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMashine : MonoBehaviour
{
    public float directionChangeInterval;

    public float attakDuration = 1f;
    public float attakCooldown = 2f;
    public float affectionDuration = 1f;

    private Rigidbody2D rb2D;
    private EnemyAIPatroling enemyAIPatrolingScript;
    private EnemyAIChasing enemyAIChasingScript;
    private Animator anim;
    private bool stateSwitchable = true;

    public enum State
    {
        Idle,
        Patroling,
        Chasing,
        Attaking, 
        Affected
    }

    public State state;

    Vector3 endPosition;
    float currentAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        enemyAIPatrolingScript = GetComponent<EnemyAIPatroling>();
        enemyAIChasingScript = GetComponent<EnemyAIChasing>();
        rb2D = GetComponent<Rigidbody2D>();
        state = State.Patroling;
        setStatebehaviour();
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        // equal to (PI * 2) / 360, the degrees to radians conversion constant
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0) * 3;
    }

    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360); // degrees

        // if currentAngle is greater than 360, loop so it starts at 0 again, keeping the value between 0 and 360
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }


    IEnumerator PatrolingState()
    {
        while (state == State.Patroling)
        {
            ChooseNewEndpoint();
            enemyAIPatrolingScript.target = endPosition;
            yield return new WaitForSeconds(directionChangeInterval);
        }
        enemyAIPatrolingScript.enabled = false;
    }

    IEnumerator AttakingState()
    {
        anim.SetTrigger("Attaking");
        yield return new WaitForSeconds(attakDuration);
        state = State.Chasing;
        setStatebehaviour();
    }

    IEnumerator AffectedState()
    {
        rb2D.velocity = Vector2.zero;
        stateSwitchable = false;
        anim.SetTrigger("DamageRecieve");
        yield return new WaitForSeconds(affectionDuration);
        stateSwitchable = true;
        state = State.Chasing;
        setStatebehaviour();
    }


    public void setStatebehaviour()
    {
        if (stateSwitchable && anim)
        {
            switch (state)
            {
                case (State.Idle):
                    enemyAIChasingScript.enabled = false;
                    anim.SetTrigger("Idle");
                    break;
                case (State.Patroling):
                    enemyAIPatrolingScript.enabled = true;
                    enemyAIChasingScript.enabled = false;
                    anim.SetTrigger("Patrouling");
                    StartCoroutine(PatrolingState());
                    break;
                case (State.Chasing):
                    Debug.Log("ChangeToChasing");
                    enemyAIPatrolingScript.enabled = false;
                    enemyAIChasingScript.enabled = true;
                    anim.SetTrigger("Chasing");
                    break;
                case (State.Attaking):
                    enemyAIChasingScript.enabled = false;
                    enemyAIPatrolingScript.enabled = false;
                    StartCoroutine(AttakingState());
                    break;
                case (State.Affected):
                    enemyAIChasingScript.enabled = false;
                    enemyAIPatrolingScript.enabled = false;
                    StartCoroutine(AffectedState());
                    break;

                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = State.Chasing;
            setStatebehaviour();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (state == State.Patroling))
        {
            state = State.Chasing;
            setStatebehaviour();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = State.Patroling;
            setStatebehaviour();
        }
    }
}
