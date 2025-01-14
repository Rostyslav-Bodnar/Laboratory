
public class CrouchLocomotion
{
    public MovementAnimationCalculation movementAnimationCalculation;

    private EventBus _eventBus;

    private float crouchSpeed = 2f;

    public CrouchLocomotion()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        movementAnimationCalculation = ServiceLocator.Current.Get<MovementAnimationCalculation>();
    }

    public void HandleCrouchMovement()
    {
        if (movementAnimationCalculation.moveAmount > 0 && movementAnimationCalculation.moveAmount < 0.5f)
            _eventBus.Invoke(new OnSpeedChanged(crouchSpeed));
        else
        {
            return;
        }
    }

}
