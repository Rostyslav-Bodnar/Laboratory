using UnityEngine.AI;
using UnityEngine;

public class IdleEnemyState : EnemyState
{
    private readonly float idleDuration; // ��������� ����� ������
    private float idleTimer; // ˳������� ����

    public IdleEnemyState(EnemyManager enemyManager, float duration = 5f) : base(enemyManager)
    {
        this.idleDuration = duration;
        this.idleTimer = 0f;
    }

    public override EnemyState Tick()
    {
        enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

        idleTimer += Time.deltaTime;

        EnemyState detectedState = enemyManager.playerDetectionController.DetectPlayer(); // ������������ ��'���� playerDetection
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
