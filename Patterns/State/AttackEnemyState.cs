
using UnityEngine;

public class AttackEnemyState : EnemyState
{
    public AttackEnemyState(EnemyManager enemyManager) : base(enemyManager) 
    {
    }

    public override EnemyState Tick()
    {
        return AttackTarget();
    }

    private EnemyState AttackTarget()
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        if (enemyManager.isPerformingAction)
            return new CombatStanceEnemyState(enemyManager);

        GetNewAttack();
        if (enemyManager.attackActionController.currentAttack != null)
        {
            if(distanceFromTarget < enemyManager.attackActionController.currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }
            else if(distanceFromTarget < enemyManager.attackActionController.currentAttack.maximumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyManager.attackActionController.currentAttack.maxAttackAngle
                    && viewableAngle >= enemyManager.attackActionController.currentAttack.minAttackAngle)
                {
                    if(enemyManager.attackActionController.currentRecoveryTime <= 0 && !enemyManager.isPerformingAction)
                    {
                        enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyManager.enemyLocomotion.enemyAnimatorController.PlayTargetAnimation(enemyManager.attackActionController.currentAttack.ActionAnimation, true);
                        enemyManager.isPerformingAction = true;
                        enemyManager.attackActionController.currentRecoveryTime = enemyManager.attackActionController.currentAttack.recoveryTime;
                        enemyManager.attackActionController.currentAttack = null;
                        return new CombatStanceEnemyState(enemyManager);
                    }
                }
            }
        }
        else
        {
            GetNewAttack();
        }
        return new CombatStanceEnemyState(enemyManager);
    }

    private void GetNewAttack()
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        int maxScore = 0;
        for (int i = 0; i < enemyManager.attackActionController.enemyAttackActions.Length; i++)
        {
            EnemyAttackActionSO enemyAttack = enemyManager.attackActionController.enemyAttackActions[i];

            if (distanceFromTarget <= enemyAttack.maximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttack.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttack.maxAttackAngle
                    && viewableAngle >= enemyAttack.minAttackAngle)
                {
                    maxScore += enemyAttack.attackScore;
                }
            }
        }

        if (maxScore == 0)
        {
            Debug.LogWarning("No suitable attack found! Distance or angle may be out of range.");
            return;
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyManager.attackActionController.enemyAttackActions.Length; i++)
        {
            EnemyAttackActionSO enemyAttack = enemyManager.attackActionController.enemyAttackActions[i];

            if (distanceFromTarget <= enemyAttack.maximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttack.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttack.maxAttackAngle
                    && viewableAngle >= enemyAttack.minAttackAngle)
                {
                    temporaryScore += enemyAttack.attackScore;

                    if (temporaryScore > randomValue)
                    {
                        enemyManager.attackActionController.currentAttack = enemyAttack;
                        Debug.Log("Assigned attack: " + enemyAttack.ActionAnimation);
                        break;
                    }
                }
            }
        }

        if (enemyManager.attackActionController.currentAttack == null)
        {
            Debug.LogWarning("Attack was not assigned, randomValue: " + randomValue);
        }
    }
}
