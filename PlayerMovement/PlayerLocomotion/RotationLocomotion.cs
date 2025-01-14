using UnityEngine;

public class RotationLocomotion
{
    private PlayerLocomotion playerLocomotion;
    private PlayerManager playerManager;
    private MovementAnimationCalculation movementAnimationCalculation;
    private EventBus _eventBus;

    private float rotationSpeed = 15f;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        movementAnimationCalculation = ServiceLocator.Current.Get<MovementAnimationCalculation>();
        playerLocomotion = ServiceLocator.Current.Get<PlayerLocomotion>();
        playerManager = ServiceLocator.Current.Get<PlayerManager>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerRotate>(HandleRotation);
    }

    private void HandleRotation(OnPlayerRotate signal)
    {
        if (!playerManager.canRotate)
            return;
        Vector3 targetDirection = Vector3.zero;

        targetDirection = playerLocomotion.cam.forward * movementAnimationCalculation.verticalInput;
        targetDirection = targetDirection + playerLocomotion.cam.right * movementAnimationCalculation.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0f;


        if (targetDirection == Vector3.zero)
        {
            targetDirection = playerManager.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(playerManager.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        playerManager.transform.rotation = playerRotation;
    }
}
