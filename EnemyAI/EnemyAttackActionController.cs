using UnityEngine;

public class EnemyAttackActionController : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    public EnemyAttackActionSO[] enemyAttackActions;
    public EnemyAttackActionSO currentAttack;

    public float currentRecoveryTime = 0f;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
    }
    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }
        if (enemyManager.isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                enemyManager.isPerformingAction = false;
            }
        }

    }
}
