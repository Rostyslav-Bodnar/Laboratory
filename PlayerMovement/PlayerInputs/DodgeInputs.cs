
public class DodgeInputs
{
    private DodgingLocomotion dodging;

    private EventBus _eventBus;

    public DodgeInputs()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        if(dodging == null)
        {
            dodging= new DodgingLocomotion();
        }
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerDodged>(HandleDodgeInput);
    }

    private void HandleDodgeInput(OnPlayerDodged action)
    {
        dodging.AttemptToPerformDodge();
    }
}

