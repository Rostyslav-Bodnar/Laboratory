
public class DeadEnemyState : EnemyState
{
    public DeadEnemyState(EnemyManager enemyManager) : base(enemyManager) { }

    public override EnemyState Tick()
    {
        return this;
    }
}
