using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyState : EnemyState
{
    public Vector3[] patrolPoints;
    private readonly float patrolPointDistance = 5f; // ³������ �� �������

    private Vector3 currentPatrolPoint; // ������� ����� ������������
    private int patrolIndex = 0; // ������ ������� �����
    private readonly float patrolSpeed = 0.5f; // �������� ������������

    public PatrolEnemyState(EnemyManager enemyManager) : base(enemyManager)
    {
        if (!enemyManager.enemyLocomotion.navMeshAgent.enabled)
            enemyManager.enemyLocomotion.navMeshAgent.enabled = true;
        GeneratePatrolPoints();
        // ������������ ��������� ����� ������������
        if (patrolPoints.Length > 0)
        {
            currentPatrolPoint = patrolPoints[patrolIndex];
        }
        else
        {
            GeneratePatrolPoints();
        }
    }

    public override EnemyState Tick()
    {
        EnemyState detectedState = enemyManager.playerDetectionController.DetectPlayer(); // ������������ ��'���� playerDetection
        if (detectedState != null)
        {
            return detectedState;
        }

        if (Vector3.Distance(enemyManager.transform.position, currentPatrolPoint) < 1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            currentPatrolPoint = patrolPoints[patrolIndex];
            return new IdleEnemyState(enemyManager, 5f); // ���������� � ���� ������ �� 5 ������
        }

        MoveToPatrolPoint();

        return this;
    }

    private void MoveToPatrolPoint()
    {
        if (currentPatrolPoint == Vector3.zero)
            return;

        enemyManager.enemyLocomotion.navMeshAgent.SetDestination(currentPatrolPoint);
        enemyManager.enemyLocomotion.enemyAnimatorController.animator.SetFloat("Vertical", patrolSpeed, 0.1f, Time.deltaTime);
        enemyManager.transform.rotation = enemyManager.enemyLocomotion.navMeshAgent.transform.rotation;
        enemyManager.enemyLocomotion.navMeshAgent.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    private void GeneratePatrolPoints()
    {
        patrolPoints = new Vector3[3];

        Vector3 thirdPoint = enemyManager.transform.position;
        Vector3 startPoint = thirdPoint + enemyManager.transform.right * patrolPointDistance;
        Vector3 secondPoint = thirdPoint + enemyManager.transform.forward * patrolPointDistance;

        patrolPoints[0] = CheckAndSetPatrolPoint(startPoint);
        patrolPoints[1] = CheckAndSetPatrolPoint(secondPoint);
        patrolPoints[2] = CheckAndSetPatrolPoint(thirdPoint);
    }

    private Vector3 CheckAndSetPatrolPoint(Vector3 point)
    {
        // ����������, �� � ����� ��������� �� ������������ �� NavMesh
        if (NavMesh.SamplePosition(point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
        {
            return navHit.position;
        }
        // ���� ����� �� ��������, ��������� ��������� ������� ������
        return enemyManager.transform.position;
    }
}
