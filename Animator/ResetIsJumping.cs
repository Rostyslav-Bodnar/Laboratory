using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsJumping : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isJumping", false);
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ///animator.SetBool("isGrounded", false);
    }
}
