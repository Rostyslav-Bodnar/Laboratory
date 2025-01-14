using UnityEngine;

public class DodgingLocomotion
{
    private PlayerLocomotion playerLocomotion;
    private PlayerManager playerManager;
    private MovementCalculation movementCalculation;
    private MovementAnimationCalculation movementAnimationCalculation;
    private Player player;

    private float dodgeSpeed = 1.5f;
    private float dodgeStaminaCost = -20f;

    private EventBus _eventBus;

    public DodgingLocomotion()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        movementAnimationCalculation = ServiceLocator.Current.Get<MovementAnimationCalculation>();
        movementCalculation = ServiceLocator.Current.Get<MovementCalculation>();   
        playerLocomotion = ServiceLocator.Current.Get<PlayerLocomotion>();
        playerManager = ServiceLocator.Current.Get<PlayerManager>();
        player = ServiceLocator.Current.Get<Player>();
    }


    public void AttemptToPerformDodge()
    {
        if (playerManager.isInteracting || player.playerStats.staminaController.stamina <= -dodgeStaminaCost)
            return;

        if (movementAnimationCalculation.moveAmount > 0)
        {
            Vector3 rollDirection = playerLocomotion.cam.transform.forward * movementAnimationCalculation.verticalInput;
            rollDirection += playerLocomotion.cam.transform.right * movementAnimationCalculation.horizontalInput;

            rollDirection.y = 0;
            rollDirection.Normalize();
            _eventBus.Invoke(new ChangeMoveDirectionSignal(dodgeSpeed));

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            playerManager.transform.rotation = playerRotation;
            _eventBus.Invoke(new ChangeRigidbodyVelocity(movementCalculation.moveDirection));

            _eventBus.Invoke(new SetBoolInAnimator("isDodging", true));
            _eventBus.Invoke(new PlayAnimationSignal("RollForward", true, false, false, false));
            _eventBus.Invoke(new OnStaminaChanged(dodgeStaminaCost));
        }
        else
        {

        }
    }

}
