
public class AttackInput
{
    private EventBus _eventBus;

    public AttackInput()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }
    void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerAttack>(Attack);
    }

    void Attack(OnPlayerAttack signal)
    {
        _eventBus.Invoke(new OnPlayerRightArmAttack());
    }

}
