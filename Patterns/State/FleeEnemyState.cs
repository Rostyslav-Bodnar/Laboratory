
public class FleeEnemyState : EnemyState
{
    public FleeEnemyState(EnemyManager enemyManager) : base(enemyManager) { }

    public override EnemyState Tick()
    {
        return this;
    }
}
