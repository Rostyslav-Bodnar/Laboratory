using UnityEngine.AI;
using UnityEngine;

public class FollowingEnemyState : EnemyState
{

    public FollowingEnemyState(EnemyManager enemyManager) : base(enemyManager) { }
    public override EnemyState Tick()
    {
        if (enemyManager.currentTarget == null)
            return new IdleEnemyState(enemyManager);
        return HandleMoveToTarget();
    }

    public EnemyState HandleMoveToTarget()
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.transform.position, enemyManager.currentTarget.transform.position);
        if (enemyManager.isPerformingAction)
        {
            enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            enemyManager.enemyLocomotion.navMeshAgent.enabled = false;
            return this;
        }
        FindPath();
        HandleRotateTowardsTarget();
        enemyManager.enemyLocomotion.navMeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.enemyLocomotion.navMeshAgent.transform.localRotation = Quaternion.identity;
        if (enemyManager.enemyLocomotion.navMeshAgent.enabled)
        {
            if (distanceFromTarget > enemyManager.enemyLocomotion.navMeshAgent.stoppingDistance)
            {
                enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                return this;

            }
            else if(distanceFromTarget <= enemyManager.enemyLocomotion.navMeshAgent.stoppingDistance)
            {
                enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return new CombatStanceEnemyState(enemyManager);
            }
        }
        else
        {
            return new IdleEnemyState(enemyManager);
        }
        return this;

    }

    private void HandleRotateTowardsTarget()
    {
        enemyManager.transform.rotation = enemyManager.enemyLocomotion.navMeshAgent.transform.rotation;
    }

    private void FindPath()
    {
        if (enemyManager.currentTarget == null)
            return;
        if (!enemyManager.enemyLocomotion.navMeshAgent.enabled)
            enemyManager.enemyLocomotion.navMeshAgent.enabled = true;
        NavMeshPath path = new NavMeshPath();
        enemyManager.enemyLocomotion.navMeshAgent.CalculatePath(enemyManager.currentTarget.transform.position, path);
        enemyManager.enemyLocomotion.navMeshAgent.SetPath(path);
    }
}
