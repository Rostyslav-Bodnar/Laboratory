using UnityEngine;

public class WalkLocomotion
{
    public MovementAnimationCalculation movementAnimationCalculation;

    private EventBus _eventBus;

    private float walkSpeed = 2f;

    public WalkLocomotion()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        movementAnimationCalculation = ServiceLocator.Current.Get<MovementAnimationCalculation>();
    }

    public void HandleWalkMovement()
    {
        if (movementAnimationCalculation.moveAmount > 0 && movementAnimationCalculation.moveAmount < 0.5f)
            _eventBus.Invoke(new OnSpeedChanged(walkSpeed));
        else
        {
            return;
        }
    }

}
