using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : StateMachineBehaviour
{
    public float stoppingDistance;
    private Transform target;
    public float attackrange = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {





        if (attackrange < Vector2.Distance(animator.transform.position, target.position)  && Vector2.Distance(animator.transform.position, target.position) < stoppingDistance)
        {
            animator.SetBool("patrol", false);
            animator.SetBool("chasing", true);

        }

        if (Vector2.Distance(animator.transform.position, target.position) < attackrange)
        {
            animator.SetBool("patrol", false);
            animator.SetBool("chasing", false);
            animator.SetBool("attack", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
