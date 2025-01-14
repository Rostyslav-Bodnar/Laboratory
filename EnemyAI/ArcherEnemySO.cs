using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DistanceEnemy/Archer")]
public class ArcherEnemySO : EnemySO
{
    [Header("Weapons")]

    public DistanceWeapon distanceWeapon;
}
