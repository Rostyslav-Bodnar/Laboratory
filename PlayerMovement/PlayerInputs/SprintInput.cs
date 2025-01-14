
public class SprintInput
{
    private SprintLocomotion sprintLocomotion;
    private PlayerManager playerManager;
    private EventBus _eventBus;
    private Player player;

    private float SprintAmount = 2f;
    private float sprintCost = 0.09f;
    private float springBorder = 10f;
    private float startSprintBorder = 40f;

    private bool isTired = false;

    public SprintInput()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        playerManager = ServiceLocator.Current.Get<PlayerManager>();
        player = ServiceLocator.Current.Get<Player>();
        if (sprintLocomotion == null)
            sprintLocomotion = new SprintLocomotion();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerSprint>(HandleSprintingInput);
    }
    private void HandleSprintingInput(OnPlayerSprint signal)
    {
        if (player.playerStats.staminaController.stamina > springBorder && !isTired && playerManager.canMove)
        {
            _eventBus.Invoke(new OnMoveAmountCalculate());
            if (sprintLocomotion.movementAnimationCalculation.moveAmount > 0)
            {
                _eventBus.Invoke(new ChangeMoveAmountSignal(SprintAmount));
            }
            sprintLocomotion.HandleWalkMovement();
            _eventBus.Invoke(new OnAnimatorCall());
            _eventBus.Invoke(new OnStaminaChanged(player.playerStats.staminaController.staminaRegeneration - sprintCost));
        }
        else
        {
            isTired = true;
            if (player.playerStats.staminaController.stamina > startSprintBorder)
                isTired = false;
            if(player.playerStats.staminaController.stamina < 30f)
                _eventBus.Invoke(new OnPlayerWalk());
            else if(player.playerStats.staminaController.stamina > 30f)
                _eventBus.Invoke(new OnPlayerRun());
        }

    }
}
