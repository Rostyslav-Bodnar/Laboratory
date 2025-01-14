using UnityEngine;

public class PlayerInputManager : MonoBehaviour, IService
{
    PlayerControls playerControls;

    [SerializeField] public Vector2 movementInput;

    private bool sprint_input;
    private bool walk_Input;
    private bool crouch_input;

    public bool canDoAction;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => 
            {
                if (canDoAction)
                    movementInput = i.ReadValue<Vector2>();
                else
                {
                    movementInput = Vector2.zero;
                }
            };

            playerControls.PlayerAction.Run.performed += i => sprint_input = true;
            playerControls.PlayerAction.Run.canceled += i => sprint_input = false;

            playerControls.PlayerAction.Walk.performed += i => walk_Input = true;
            playerControls.PlayerAction.Walk.canceled += i => walk_Input = false;

            playerControls.PlayerAction.Crouch.performed += i => crouch_input = true;
            playerControls.PlayerAction.Crouch.canceled += i => crouch_input = false;

            playerControls.PlayerAction.Defence.performed += i =>
            { 
                if(canDoAction)
                {
                    _eventBus.Invoke(new OnPlayerDefence());
                }
            };

            playerControls.PlayerAction.Defence.canceled += i => 
            {
                _eventBus.Invoke(new SetBoolInAnimator("isBlocking", false));

            };
            playerControls.PlayerAction.Jump.performed += i =>
            {
                if (canDoAction)
                {
                    _eventBus.Invoke(new OnPlayerJumped());
                }
            };
            playerControls.PlayerAction.Dodge.performed += i =>
            {
                if (canDoAction)
                {
                    _eventBus.Invoke(new OnPlayerDodged());
                }
            };

            playerControls.PlayerAction.Attack.performed += i =>
            {
                if (canDoAction)
                {
                    _eventBus.Invoke(new OnPlayerAttack());
                }
            };
            /*playerControls.PlayerAction.Defence.performed += i =>
            {
                if (canDoAction)
                    _eventBus.Invoke(new OnPlayerDefence());
            };*/

        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInput()
    {
        

        if (walk_Input)
        {
            _eventBus.Invoke(new OnPlayerWalk());
        }
        if (sprint_input)
        {
            _eventBus.Invoke(new OnPlayerSprint());
        }
        if (crouch_input)
        {
            _eventBus.Invoke(new OnPlayerCrouch());
        }
        else
        {
            _eventBus.Invoke(new OnPlayerRun());
        }
        
    }
    public void ResetInputs()
    {
        movementInput = Vector2.zero;
        sprint_input = false;
        walk_Input = false;
        crouch_input = false;
    }

}
