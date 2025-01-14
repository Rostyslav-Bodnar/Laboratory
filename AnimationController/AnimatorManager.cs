using UnityEngine;

public class AnimatorManager : MonoBehaviour, IService
{
    CharacterManager characterManager;

    public Animator animator;
    int horizontal;
    int vertical;
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        characterManager = GetComponent<CharacterManager>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<PlayAnimationSignal>(OnAnimationPlay);
        _eventBus.Subscribe<SetBoolInAnimator>(SetBoolInAnimations);
        _eventBus.Subscribe<OnAnimatorUpdate>(OnAnimatorsValueUpdate);
        _eventBus.Subscribe<SetTriggerInAnimator>(SetTrigger);
        _eventBus.Subscribe<SetFloatInAnimator>(SetFloat);
        _eventBus.Subscribe<SetRootMotionInAnimator>(SetRootMotion);
        _eventBus.Subscribe<SetAnimatorControllerSignal>(SetAnimatorController);

    }

    private void OnAnimationPlay(PlayAnimationSignal signal)
    {

        PlayTargetAnimation(signal.nameOfAnimation, signal.isInteracting, signal.applyRootMotion, signal.canRotate, signal.canMove);
    }

    private void SetBoolInAnimations(SetBoolInAnimator signal)
    {
        animator.SetBool(signal.nameOfBool, signal.state);
    }

    private void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        animator.applyRootMotion = applyRootMotion;
        characterManager.canMove = canMove;
        characterManager.canRotate = canRotate;
        animator.CrossFade(targetAnimation, 0.2f);
        characterManager.isInteracting = isInteracting;
        animator.SetBool("isInteracting", isInteracting);

    }

    private void OnAnimatorsValueUpdate(OnAnimatorUpdate signal)
    {
        UpdateAnimatorValues(signal.horizontal, signal.vertical);
    }
    
    private void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontal =horizontalMovement;
        float snappedVertical = verticalMovement;
        /*if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.49f;
        else if (horizontalMovement > 0.55f)
            snappedHorizontal = 1f;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.49f;
        else if (horizontalMovement > 0.55f)
            snappedHorizontal = -1f;
        else
            snappedHorizontal = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
            snappedVertical = 0.49f;
        else if (verticalMovement > 0.55f)
            snappedVertical = 1f;
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
            snappedVertical = -0.49f;
        else if (verticalMovement > 0.55f)
            snappedVertical = -1f;
        else
            snappedVertical = 0;

        if(isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2f;
        }*/
        /*if(isWalking)
        {
            if (horizontalMovement > 0)
                snappedHorizontal = 0.49f;
            else if (horizontalMovement < 0)
                snappedHorizontal = -0.49f;
            else
                snappedHorizontal = 0;

            if (verticalMovement > 0)
                snappedVertical = 0.49f;
            else if (verticalMovement < 0)
                snappedVertical = -0.49f;
            else
                snappedVertical = 0;
            snappedHorizontal = horizontalMovement;
            snappedVertical = 0.49f;
        }*/
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    private void SetTrigger(SetTriggerInAnimator signal)
    {
        animator.SetTrigger(signal.nameOfTrigger);
    }

    private void SetFloat(SetFloatInAnimator signal)
    {
        animator.SetFloat(signal.nameOfFloat, signal.value);
    }

    private void SetRootMotion(SetRootMotionInAnimator signal)
    {
        animator.applyRootMotion = signal.applyRootMotion;
    }

    private void SetAnimatorController(SetAnimatorControllerSignal signal)
    {
        animator.runtimeAnimatorController = signal.AnimatorOverrideController;
    }


}
