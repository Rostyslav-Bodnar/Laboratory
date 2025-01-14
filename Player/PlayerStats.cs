
public class PlayerStats
{
    public StaminaController staminaController;
    public HealthController healthController;
    public DamageController damageController;

    public PlayerStats()
    {
        Init();
    }
    private void Init()
    {
        staminaController = new StaminaController();
        healthController = new HealthController();
        damageController = new DamageController();
    }
}
