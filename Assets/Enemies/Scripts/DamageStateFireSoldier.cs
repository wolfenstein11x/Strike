using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStateFireSoldier : StateMachineBehaviour
{
    FireSoldier fireSoldier;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireSoldier = animator.GetComponent<FireSoldier>();
        fireSoldier.Halt();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // only re-enable movement if soldier is still alive (prevent corpses from following player)
        if (!fireSoldier.GetComponent<EnemyHealth>().IsDead())
        {
            fireSoldier.UnHalt();
        }

        // if soldier is dead, turn off flame thrower
        else
        {
            fireSoldier.ToggleFlamethrower(false);
        }
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
