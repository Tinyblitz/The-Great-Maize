using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTwoBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerControlScript.instance.canReceiveInput = true;
        //PlayerController.instance._speed = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerControlScript.instance.inputReceived && PlayerControlScript.instance.inputType == 1)
        {
            animator.SetTrigger("attackThree");
            PlayerControlScript.instance.InputManager();
            PlayerControlScript.instance.inputReceived = false;
            PlayerControlScript.instance.inputType = 0;
        }  else if (PlayerControlScript.instance.inputReceived && PlayerControlScript.instance.inputType == 3)
        {
            animator.SetTrigger("dodgeRoll");
            PlayerControlScript.instance.InputManager();
            PlayerControlScript.instance.inputReceived = false;
            PlayerControlScript.instance.inputType = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //PlayerController.instance._speed = 4.5f;
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
