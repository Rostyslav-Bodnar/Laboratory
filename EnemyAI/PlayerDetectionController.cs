using UnityEngine;

public class PlayerDetectionController
{
    private readonly EnemyManager enemyManager;
    private AlertBarPresenter alertBarController;

    public Vector3 lastKnownPlayerPosition;

    public PlayerDetectionController(EnemyManager enemyManager, UI_AlertBar alertBar)
    {
        this.enemyManager = enemyManager;
        alertBarController = new AlertBarPresenter(alertBar);
    }

    public EnemyState DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(
            enemyManager.transform.position,
            enemyManager.enemy.detectionRadius,
            enemyManager.enemyLocomotion.detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager character = colliders[i].GetComponentInParent<PlayerManager>();
            if (character != null)
            {
                Vector3 targetDirection = character.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                if (viewableAngle > enemyManager.enemy.minViewableAngle && viewableAngle < enemyManager.enemy.maxViewableAngle)
                {
                    if (!Physics.Linecast(enemyManager.transform.position, character.transform.position, enemyManager.obstaclesLayer))
                    {
                        alertBarController.FillAlertBar();
                        if(alertBarController.alertBarModel.alertBarValue >= 0.8f && alertBarController.alertBarModel.detectionBarValue <= 1f)
                        {
                            lastKnownPlayerPosition = character.transform.position;
                            return new AlertEnemyState(enemyManager);
                        }
                        if (alertBarController.alertBarModel.detectionBarValue >= 1f)
                        {
                            enemyManager.currentTarget = character;
                            if (!enemyManager.enemyLocomotion.navMeshAgent.enabled)
                            {
                                enemyManager.enemyLocomotion.navMeshAgent.enabled = true;
                            }
                            return new FollowingEnemyState(enemyManager); // Переходимо в стан переслідування
                        }
                    }
                }
            }
        }

        // Гравець не виявлений, зменшуємо значення alertBar
        alertBarController.ReduceAlertBar();

        // Гравець не виявлений
        return null;
    }
}
