using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        START,PATROL,CHASE,ATTACK
    }

    public List<Transform> waypoints = new List<Transform>();
    GameObject target;
    Enemy enemy;
    Animator anim;
    Rigidbody2D rb;

    public float speed;
    public EnemyState state;
    public LayerMask playerLayer;
    public Transform attackPoint;
    float nextAttackTime = 0;

    int index = 0;
    int nextIndexAdder = 1;

    bool canMove = true;
    bool isChase;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.START;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsChasing();

        if (canMove && !isChase)
        {
            StartCoroutine(Patrol());
        }
        else if (isChase)
        {
            Chase();
        }
    }

    public IEnumerator Patrol()
    {
        state = EnemyState.PATROL;
        Transform goalPoint = waypoints[index];

        if (transform.position.x < goalPoint.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector2 goal = Vector3.MoveTowards(transform.position, goalPoint.position, speed * Time.fixedDeltaTime);
        anim.SetBool("isWalking", true);
        rb.MovePosition(goal);

        if (Vector3.Distance(transform.position, goalPoint.position) < .1f)
        {
            canMove = false;
            anim.SetBool("isWalking", false);

            yield return new WaitForSeconds(1f);

            if (index == 0)
            {
                nextIndexAdder = 1;
            }
            else if (index == waypoints.Count - 1)
            {
                nextIndexAdder = -1;
            }

            index += nextIndexAdder;
            canMove = true;
        }
    }

    public void Chase()
    {
        state = EnemyState.CHASE;
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("TakeDamage"))
        {
            if (transform.position.x < target.transform.position.x)
            {
                rb.velocity = new Vector2(speed+1, 0f);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-speed-1, 0f);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Vector2.Distance(transform.position, target.transform.position) <= enemy.attackRange)
            {
                Attacking();
            }
        }
    }

    public void Attacking()
    {
        state = EnemyState.ATTACK;
        rb.velocity = Vector2.zero;
        anim.SetBool("isWalking", false);
        if(Time.time >= nextAttackTime)
        {
            anim.SetBool("isAttacking", true);
            nextAttackTime = Time.time + 1f / enemy.attackRate;
        }
    }

    public void Attack()
    {
        Collider2D players = Physics2D.OverlapCircle(attackPoint.transform.position, enemy.attackRange, playerLayer);

        if(players!=null)
        {
            players.GetComponent<CharacterStatus>().TakeDamage();
        }
    }

    void IsChasing()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance < enemy.agroDistance && !CharacterStatus.isDead)
        {
            isChase = true;
        }
        else
        {
            isChase = false;
        }
    }
}
