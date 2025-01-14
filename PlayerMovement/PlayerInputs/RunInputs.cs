public class RunInputs
{
    private EventBus _eventBus;
    private Player player;

    private float runSpeed = 5f;
    private float runCost = 0.04f;

    public RunInputs()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        player = ServiceLocator.Current.Get<Player>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerRun>(HandingRunnigInput);
    }
    public void HandingRunnigInput(OnPlayerRun signal)
    {
        if (player.playerStats.staminaController.stamina > 20f)
        {
            _eventBus.Invoke(new OnMoveAmountCalculate());
            _eventBus.Invoke(new OnSpeedChanged(runSpeed));
            _eventBus.Invoke(new OnAnimatorCall());
            _eventBus.Invoke(new OnStaminaChanged(player.playerStats.staminaController.staminaRegeneration - runCost));
        }
        else
        {
            _eventBus.Invoke(new OnPlayerWalk());
        }
    }
}
