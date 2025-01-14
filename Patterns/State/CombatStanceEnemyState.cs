using UnityEngine;

public class CombatStanceEnemyState : EnemyState
{
    public CombatStanceEnemyState(EnemyManager enemyManager) : base(enemyManager) { }

    public override EnemyState Tick()
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.transform.position, enemyManager.currentTarget.transform.position);
        if(enemyManager.isPerformingAction)
        {
            enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

        }
        if (distanceFromTarget > enemyManager.enemyLocomotion.navMeshAgent.stoppingDistance)
        {
            return new FollowingEnemyState(enemyManager);
        }
        else if(distanceFromTarget <= enemyManager.enemyLocomotion.navMeshAgent.stoppingDistance)
        {
            return new AttackEnemyState(enemyManager);
        }
        return this;
    }
}
