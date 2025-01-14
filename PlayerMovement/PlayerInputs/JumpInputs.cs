using UnityEngine;

//Баг зі стрибком коли стаміна менше дозволеного
public class JumpInputs
{
    private JumpLocomotion jump;
    private EventBus _eventBus;

    public JumpInputs()
    {
        Init();
    }

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        if(jump == null)
            jump = new JumpLocomotion();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerJumped>(HandleJumpInput);
    }

    public void HandleJumpInput(OnPlayerJumped onPlayerJumped)
    {
        jump.HandleJumping();
    }
}
