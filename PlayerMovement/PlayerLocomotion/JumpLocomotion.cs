using UnityEngine;

public class JumpLocomotion
{
    private PlayerLocomotion playerLocomotion;

    private float jumpHeight = 3f;
    private float gravityIntensity = -15f;
    private float jumpCost = 15f;

    private Vector3 moveDirection;

    private EventBus _eventBus;
    private Player player;

    public JumpLocomotion()
    {
        Init();
    }
    
    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        playerLocomotion = ServiceLocator.Current.Get<PlayerLocomotion>();
        player = ServiceLocator.Current.Get<Player>();
    }


    public void HandleJumping()
    {
        if (playerLocomotion.isGrounded && player.playerStats.staminaController.stamina > jumpCost)
        {
            playerLocomotion.isGrounded = false;
            _eventBus.Invoke(new SetBoolInAnimator("isJumping", true));
            _eventBus.Invoke(new PlayAnimationSignal("Jump", false, true));
            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            _eventBus.Invoke(new ChangeRigidbodyVelocity(playerVelocity));
            _eventBus.Invoke(new OnStaminaChanged(-jumpCost));

        }
        else
        {
            return;
        }
    }
}
