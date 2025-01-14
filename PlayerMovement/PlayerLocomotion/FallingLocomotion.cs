using UnityEngine;

public class FallingLocomotion
{
    private PlayerLocomotion playerLocomotion;
    private PlayerManager playerManager;
    private MovementAnimationCalculation movementAnimationCalculation;

    private EventBus _eventBus;

    private float inAirTimer;
    private float fallingVelocity = 33f;
    private float leapingVelocity = 3f;
    private float raycastHeightOffset = 0.5f;
    private LayerMask groundLayer;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        movementAnimationCalculation = ServiceLocator.Current.Get<MovementAnimationCalculation>();
        playerLocomotion = ServiceLocator.Current.Get<PlayerLocomotion>();
        playerManager = ServiceLocator.Current.Get<PlayerManager>();
        groundLayer = playerLocomotion.groundLayer;
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerFall>(HandleFallingAndLanding);
    }

    public void HandleFallingAndLanding(OnPlayerFall signal)
    {
        RaycastHit hit;
        Vector3 raycastOrigin = playerLocomotion.transform.position;
        Vector3 targetPosition;
        raycastOrigin.y = raycastOrigin.y + raycastHeightOffset;
        targetPosition = playerLocomotion.transform.position;
        if (!playerLocomotion.isGrounded)
        {
            if (!playerManager.isInteracting)
            {
                _eventBus.Invoke(new PlayAnimationSignal("Falling", true, false));
                _eventBus.Invoke(new SetBoolInAnimator("isGrounded", false));
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerLocomotion.playerRigidbody.AddForce(playerLocomotion.transform.forward * leapingVelocity);
            playerLocomotion.playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!playerLocomotion.isGrounded && !playerManager.isInteracting)
            {
                _eventBus.Invoke(new PlayAnimationSignal("Land", true, false, false, true));

            }

            Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;
            inAirTimer = 0f;
            playerLocomotion.isGrounded = true;
            _eventBus.Invoke(new SetBoolInAnimator("isGrounded", true));
        }
        else
        {
            playerLocomotion.isGrounded = false;
            _eventBus.Invoke(new SetBoolInAnimator("isGrounded", false));

        }

        if (playerLocomotion.isGrounded)
        {
            if (playerManager.isInteracting || movementAnimationCalculation.moveAmount > 0f)
            {
                playerLocomotion.transform.position = Vector3.Lerp(playerLocomotion.transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                playerLocomotion.transform.position = targetPosition;
            }
        }
    }
}
