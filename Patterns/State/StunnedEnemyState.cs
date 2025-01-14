
public class StunnedEnemyState : EnemyState
{
    public StunnedEnemyState(EnemyManager enemyManager) : base(enemyManager) { }
    public override EnemyState Tick()
    {
        return this;
    }
}
