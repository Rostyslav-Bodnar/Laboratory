using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsCombat : StateMachineBehaviour
{
    PlayerManager playerManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerManager = animator.GetComponent<PlayerManager>();
        animator.applyRootMotion = false;
        playerManager.canMove = true;
        playerManager.canRotate = true;
        playerManager.isInteracting = false;
    }
}
