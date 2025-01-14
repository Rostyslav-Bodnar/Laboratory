using UnityEngine;

public class WalkInput
{
    private WalkLocomotion walkLocomotion;
    private Player player;

    private float walkAmount = 0.49f;
    private float walkCost = 0.02f;

    private EventBus _eventBus;

    public WalkInput()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        player = ServiceLocator.Current.Get<Player>();
        if(walkLocomotion == null)
            walkLocomotion = new WalkLocomotion();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerWalk>(HandleWalkInput);
    }

    private void HandleWalkInput(OnPlayerWalk signal)
    {
        _eventBus.Invoke(new OnMoveAmountCalculate());
        if (walkLocomotion.movementAnimationCalculation.moveAmount > 0)
        {
            _eventBus.Invoke(new ChangeMoveAmountSignal(walkAmount));
        }
        walkLocomotion.HandleWalkMovement();
        _eventBus.Invoke(new OnAnimatorCall());
        _eventBus.Invoke(new OnStaminaChanged(player.playerStats.staminaController.staminaRegeneration - walkCost));

    }

}
