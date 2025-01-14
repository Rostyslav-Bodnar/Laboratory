using UnityEngine;

public class PlayerStatsServiceLocator : MonoBehaviour
{
    [SerializeField] private UI_Stamina stamina;

    private Player player;
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = new EventBus();
        player = new Player();

        Register();
        Init();
    }

    private void Init()
    {
        player.Init();
        stamina.Init();
    }

    private void Register()
    {
        ServiceLocator.Init();
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(player);
        ServiceLocator.Current.Register(stamina);
    }
}
