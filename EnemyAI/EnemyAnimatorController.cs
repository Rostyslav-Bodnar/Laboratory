using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    public Animator animator;
    public EnemyManager enemyManager;
    public EnemyLocomotion EnemyLocomotion;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyManager = GetComponent<EnemyManager>();
    }
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        animator.applyRootMotion = applyRootMotion;
        animator.CrossFade(targetAnimation, 0.2f);
        enemyManager.isPerformingAction = isInteracting;
        //animator.SetBool("isPerformingAction", isInteracting);

    }

}
