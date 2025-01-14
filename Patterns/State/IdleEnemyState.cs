using UnityEngine.AI;
using UnityEngine;

public class IdleEnemyState : EnemyState
{
    private readonly float idleDuration; // Тривалість стану спокою
    private float idleTimer; // Лічильник часу

    public IdleEnemyState(EnemyManager enemyManager, float duration = 5f) : base(enemyManager)
    {
        this.idleDuration = duration;
        this.idleTimer = 0f;
    }

    public override EnemyState Tick()
    {
        enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

        idleTimer += Time.deltaTime;

        EnemyState detectedState = enemyManager.playerDetectionController.DetectPlayer(); // Використання об'єкта playerDetection
        if (detectedState != null)
        {
            return detectedState;
        }

        if (idleTimer >= idleDuration)
        {
            return new PatrolEnemyState(enemyManager);
        }
        return this;
    }
}
