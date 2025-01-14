using UnityEngine;

public class PlayerLocomotion : CharacterLocomotion
{
    public Transform cam;
    public Rigidbody playerRigidbody;

    public LayerMask groundLayer;

    [Header("Flags")]
    //public bool isJumping;
    public bool isGrounded;

    private EventBus _eventBus;

    public override void Init()
    {
        base.Init();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        cam = Camera.main.transform;
        playerRigidbody = GetComponent<Rigidbody>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<ChangeRigidbodyVelocity>(SetRigidbodyVelocity);
    }

    public void HandleAllMovement()
    {
        _eventBus.Invoke(new OnPlayerFall());
        _eventBus.Invoke(new OnPlayerRotate());
    }

    private void SetRigidbodyVelocity(ChangeRigidbodyVelocity signal)
    {
        playerRigidbody.velocity = signal.velocity;
    }
}
