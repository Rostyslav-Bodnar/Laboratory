
public class CrouchInput
{

    private CrouchLocomotion crouchLocomotion;

    private float walkAmount = 0.25f;
    private float crouchCost = 0.02f;

    private EventBus _eventBus;
    private Player player;

    public CrouchInput()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        player = ServiceLocator.Current.Get<Player>();
        if (crouchLocomotion == null)
            crouchLocomotion = new CrouchLocomotion();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerCrouch>(HandleCrouchInput);
    }

    public void HandleCrouchInput(OnPlayerCrouch signal)
    {
        _eventBus.Invoke(new OnMoveAmountCalculate());
        if (crouchLocomotion.movementAnimationCalculation.moveAmount > 0)
        {
            _eventBus.Invoke(new ChangeMoveAmountSignal(walkAmount));
        }
        crouchLocomotion.HandleCrouchMovement();
        _eventBus.Invoke(new OnAnimatorCall());
        _eventBus.Invoke(new OnStaminaChanged(player.playerStats.staminaController.staminaRegeneration - crouchCost));
    }
}
