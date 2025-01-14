public class PlayerInputInit
{

    private EventBus _eventBus;

    private DodgeInputs dodgeInputs;
    private JumpInputs jumpInputs;
    private RunInputs runInputs;
    private CrouchInput crouchInputs;
    private WalkInput walkInput;
    private SprintInput sprintInputs;
    private AttackInput attackInputs;

    public void Init()
    {
        jumpInputs = new JumpInputs();
        runInputs = new RunInputs();
        crouchInputs = new CrouchInput();
        walkInput = new WalkInput();
        sprintInputs = new SprintInput();
        dodgeInputs = new DodgeInputs();
        attackInputs = new AttackInput();
    }
}
