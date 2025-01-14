using UnityEngine;

public class MovementAnimationCalculation : IService
{
    public PlayerInputManager playerInputManager;

    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;
    public Vector2 movementInput;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        playerInputManager = ServiceLocator.Current.Get<PlayerInputManager>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<ChangeMoveAmountSignal>(OnMoveAmountChanged);
        _eventBus.Subscribe<OnMoveAmountCalculate>(OnMoveAmountCalculate);
        _eventBus.Subscribe<OnAnimatorCall>(OnAnimatorCall);
    }

    private void OnMoveAmountChanged(ChangeMoveAmountSignal signal)
    {
        SetMoveAmount(signal.moveAmount);
    }

    public void OnMoveAmountCalculate(OnMoveAmountCalculate signal)
    {
        CalculateMoveAmount();
    }

    public void CalculateMoveAmount()
    {
        movementInput = playerInputManager.movementInput;
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        _eventBus.Invoke(new CalculateMovementSignal());
    }

    public void SetMoveAmount(float value)
    {
        moveAmount = value;
    }

    private void OnAnimatorCall(OnAnimatorCall signal)
    {
        _eventBus?.Invoke(new OnAnimatorUpdate(0, moveAmount));
    }

}
