
using UnityEngine;

public class SprintLocomotion
{
    public MovementAnimationCalculation movementAnimationCalculation;

    private EventBus _eventBus;

    private float sprintSpeed = 10f;

    public SprintLocomotion()
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
        if (movementAnimationCalculation.moveAmount > 1)
            _eventBus.Invoke(new OnSpeedChanged(sprintSpeed));
        else
        {
            return;
        }
    }
}
