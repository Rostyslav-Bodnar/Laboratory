using UnityEngine;

public class AlertEnemyState : EnemyState
{
    private Vector3 targetPosition;
    public AlertEnemyState(EnemyManager enemyManager) : base(enemyManager) 
    {
        targetPosition = enemyManager.playerDetectionController.lastKnownPlayerPosition;
    }

    public override EnemyState Tick()
    {
        if (enemyManager.isPerformingAction)
            return this;

        EnemyState curentTarget = enemyManager.playerDetectionController.DetectPlayer();
        if (curentTarget != null)
        {
            return curentTarget;
        }

        MoveToTargetPosition();

        // Якщо ворог досяг останньої відомої позиції, він повертається у стан пошуку чи очікування
        if (Vector3.Distance(enemyManager.transform.position, targetPosition) <= enemyManager.enemyLocomotion.navMeshAgent.stoppingDistance)
        {
            enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
            return new IdleEnemyState(enemyManager); // Повертаємося у стан очікування або пошуку
        }
        return this;
    }

    private void MoveToTargetPosition()
    {
        if (!enemyManager.enemyLocomotion.navMeshAgent.enabled)
        {
            enemyManager.enemyLocomotion.navMeshAgent.enabled = true;
        }
        enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0.25f, 0.1f, Time.deltaTime);
        enemyManager.enemyLocomotion.navMeshAgent.SetDestination(targetPosition);

        enemyManager.transform.rotation = enemyManager.enemyLocomotion.navMeshAgent.transform.rotation;

        enemyManager.enemyLocomotion.navMeshAgent.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

    }

}
