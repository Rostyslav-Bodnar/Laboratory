
public abstract class EnemyState
{
    protected EnemyManager enemyManager;

    public EnemyState(EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
    }

    public abstract EnemyState Tick();


}
