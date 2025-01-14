using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MeleeEnemy/OneHandedWeaponEnemy")]
public class MeleeEnemySO : EnemySO
{
    [Header("Weapon")]

    public Shield shield;
    public MeleeWeapon meleeWeapon;
}
