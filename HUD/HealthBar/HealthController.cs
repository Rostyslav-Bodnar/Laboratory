

public class HealthController
{
    public float health;
    public float maxHealth = 100f;
    //public float healthRegeneration = 0.05f;
    private EventBus _eventBus;
    public HealthController()
    {
        this.health = maxHealth;
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnHealthChanged>(ChangeHealth, 1);
    }

    private void ChangeHealth(OnHealthChanged signal)
    {
        health += signal.value;
        if (health > maxHealth)
            health = maxHealth;
        if (health < 0)
            health = 0;
    }

}
