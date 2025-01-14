using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    PlayerManager characterManager;

    public string isInteractingBool;
    public bool isInteractingStatus;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if(characterManager == null)
            characterManager = animator.GetComponent<PlayerManager>();

        animator.SetBool("isInteracting", isInteractingStatus);
        characterManager.canMove = true;
        characterManager.canRotate = true;
        characterManager.isInteracting = false;

    }


}
