using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
    float attackRange = 2.5f;
    float attackRate = .75f;
    float nextAttackTime = 0f;
    int attack = 0;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(rb.position, player.position) >= attackRange)
        {
            animator.SetBool("isChasing", true);
        }

        if(Vector3.Distance(rb.position, player.position) <= attackRange && attack == 0)
        {

            if (rb.position.x > player.position.x)
            {
                rb.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
            }

            if (Time.time >= nextAttackTime)
            {
                attack = Random.Range(1, 4);

                switch (attack)
                {
                    case 1:
                        animator.SetTrigger("Attack1");
                        break;
                    case 2:
                        animator.SetTrigger("Attack2");
                        break;
                    case 3:
                        animator.SetTrigger("Attack3");
                        break;
                }
            }
        }

        if (CharacterStatus.isDead)
        {
            animator.SetTrigger("CharacterDead");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = 0;
        nextAttackTime = Time.time + 1f / attackRate;
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
    }
}
