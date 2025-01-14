using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private UI_AlertBar alertBar;

    public EnemySO enemy;
    public EnemyLocomotion enemyLocomotion;
    public bool isPerformingAction;

    public EnemyState currentEnemyState;

    public PlayerManager currentTarget;

    public LayerMask obstaclesLayer;
    public EnemyAttackActionController attackActionController;
    public PlayerDetectionController playerDetectionController;
    private void Awake()
    {
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        currentEnemyState = new IdleEnemyState(this);
        attackActionController = GetComponent<EnemyAttackActionController>();
        playerDetectionController = new PlayerDetectionController(this, alertBar);
    }
    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {

       if (currentEnemyState != null)
        {
            EnemyState nextState = currentEnemyState.Tick();
            if (nextState != null)
            {
                SwitchState(nextState);
            }
        }

    }

    private void SwitchState(EnemyState state)
    {
        currentEnemyState = state;
    }

}
