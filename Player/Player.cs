using UnityEngine;

public class Player : IService
{
    public PlayerStats playerStats;

    public void Init()
    {
        playerStats = new PlayerStats();
    }

}
