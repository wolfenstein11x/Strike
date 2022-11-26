using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateFireSoldier : StateMachineBehaviour
{
    FireSoldier fireSoldier;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("chase");
        fireSoldier = animator.GetComponent<FireSoldier>();
        fireSoldier.SetRunning(true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireSoldier.UpdateAnimator();
        fireSoldier.ChasePlayer();

        if (fireSoldier.InFiringRange())
        {
            // start shooting
            animator.SetTrigger("attack");
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireSoldier.SetRunning(false);
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
