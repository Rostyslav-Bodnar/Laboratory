using UnityEngine;

public class StaminaController
{
    public float stamina;
    public float maxStamina = 100f;
    public float staminaRegeneration = 0.05f;
    private EventBus _eventBus;
    public StaminaController()
    {
        this.stamina = maxStamina;
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnStaminaChanged>(ChangeStamina, 1);
    }

    private void ChangeStamina(OnStaminaChanged signal) 
    {
        stamina += signal.value;
        if (stamina > maxStamina)
            stamina = maxStamina;
        if(stamina < 0)
            stamina = 0;
    }

}
