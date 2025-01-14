using UnityEngine;

public class MovementCalculation : IService
{
    private PlayerLocomotion playerLocomotion;
    private MovementAnimationCalculation movementAnimationCalculation;
    private PlayerManager playerManager;
    public Vector3 moveDirection;
    private Player player;

    private float speed = 5f;

    private EventBus _eventBus;

    public void Init()
    {
        movementAnimationCalculation = ServiceLocator.Current.Get<MovementAnimationCalculation>();
        _eventBus = ServiceLocator.Current.Get<EventBus>(); 
        playerManager = ServiceLocator.Current.Get<PlayerManager>();
        playerLocomotion = ServiceLocator.Current.Get<PlayerLocomotion>();
        player = ServiceLocator.Current.Get<Player>();

        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<CalculateMovementSignal>(CalculateMovement);
        _eventBus.Subscribe<ChangeMoveDirectionSignal>(ChangeMoveDirection);
        _eventBus.Subscribe<OnSpeedChanged>(ChangeSpeed);
    }

    private void CalculateMovement(CalculateMovementSignal signal)
    {
        HandleMovement();
    }

    private void ChangeMoveDirection(ChangeMoveDirectionSignal signal)
    {
        signal.moveDirection = moveDirection;
        moveDirection = moveDirection * signal.speed;
    }

    private void HandleMovement()
    {
        if (!playerManager.canMove || playerManager.isInteracting)
            return;
        moveDirection = playerLocomotion.cam.forward * movementAnimationCalculation.verticalInput;
        moveDirection = moveDirection + playerLocomotion.cam.right * movementAnimationCalculation.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0f;
        if(moveDirection.x == 0f && moveDirection.z == 0f)
            _eventBus.Invoke(new OnStaminaChanged(player.playerStats.staminaController.staminaRegeneration));

        if (movementAnimationCalculation.moveAmount > 0)
        {
            moveDirection = moveDirection * speed;
        }
        Vector3 movementVelocity = moveDirection;

        _eventBus.Invoke(new ChangeRigidbodyVelocity(movementVelocity));
    }

    private void ChangeSpeed(OnSpeedChanged signal)
    {
        speed = signal.speed;
    }

}
