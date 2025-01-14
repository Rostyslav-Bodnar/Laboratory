using UnityEngine;

public abstract class Weapon : HandedItem
{
    [Header("Animations")]
    public AnimatorOverrideController weaponAnimator;

    public float damage;
    public bool isTwoHanded;
    public int damageAbsorption;
}
