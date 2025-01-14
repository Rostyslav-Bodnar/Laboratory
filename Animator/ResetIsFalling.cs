using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsFalling : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isGrounded", true);
    }
}
