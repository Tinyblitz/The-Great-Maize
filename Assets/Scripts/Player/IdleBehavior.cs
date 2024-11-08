using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerControlScript.instance.inputReceived && PlayerControlScript.instance.inputType == 1)
        {
            animator.SetTrigger("attackOne");
            PlayerControlScript.instance.InputManager();
            PlayerControlScript.instance.inputReceived = false;
            PlayerControlScript.instance.inputType = 0;
        }
        else if (PlayerControlScript.instance.inputReceived && PlayerControlScript.instance.inputType == 2)
        {
            animator.SetTrigger("castRecipe");
            PlayerControlScript.instance.InputManager();
            PlayerControlScript.instance.inputReceived = false;
            PlayerControlScript.instance.inputType = 0;
        }
        else if (PlayerControlScript.instance.inputReceived && PlayerControlScript.instance.inputType == 3)
        {
            animator.SetTrigger("dodgeRoll");
            PlayerControlScript.instance.InputManager();
            PlayerControlScript.instance.inputReceived = false;
            PlayerControlScript.instance.inputType = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   //verride public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //
    //  
   //

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
