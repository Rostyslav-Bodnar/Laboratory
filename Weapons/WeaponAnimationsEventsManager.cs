using UnityEngine;

public class WeaponAnimationsEventsManager : MonoBehaviour
{
    [SerializeField] private WeaponHandSlot leftWeaponHandSlot;
    [SerializeField] private WeaponHandSlot rightWeaponHandSlot;


    [SerializeField] private LeftHandedItemEquippedSlotScript leftWeaponEquippedSlot;
    [SerializeField] private RightHandedItemEquippedSlotScript rightWeaponEquippedSlot;
    [SerializeField] private BackWeaponEquippedSlotScript backWeaponEquippedSlot;



    #region Open/Close DamageCollider
    public void OpenRightDamageCollider()
    {
        rightWeaponEquippedSlot.weaponHandSlot.OpenDamageCollider();
    }

    public void CloseRightDamageCollider()
    {
        rightWeaponEquippedSlot.weaponHandSlot.CloseDamageCollider();
    }

    public void OpenLeftDamageCollider()
    {
        leftWeaponEquippedSlot.weaponHandSlot.OpenDamageCollider();
    }

    public void CloseLeftDamageCollider()
    {
        leftWeaponEquippedSlot.weaponHandSlot.CloseDamageCollider();
    }
    #endregion

    #region Draw/Sheath Weapon
    public void SheathLeftWeapon()
    {
        leftWeaponEquippedSlot.shield.isInHand = false;
        leftWeaponEquippedSlot.weaponHandSlot.SheathWeapon();
    }
    public void SheathRightWeapon()
    {
        rightWeaponEquippedSlot.weapon.isInHand = false;
        rightWeaponEquippedSlot.weaponHandSlot.SheathWeapon();
        rightWeaponEquippedSlot.attacker.UninitializeWeapon();
    }

    public void SheathBackWeapon()
    {
        backWeaponEquippedSlot.weapon.isInHand=false;
        backWeaponEquippedSlot.weaponHandSlot.SheathWeapon();
        backWeaponEquippedSlot.attacker.UninitializeWeapon();
    }

    public void DrawLeftWeapon()
    {
        leftWeaponEquippedSlot.shield.isInHand = true;
        leftWeaponEquippedSlot.weaponHandSlot.DrawWeapon();
    }
    public void DrawRightWeapon()
    {
        rightWeaponEquippedSlot.weapon.isInHand = true;
        CheckHands(rightWeaponEquippedSlot.weapon);
        rightWeaponEquippedSlot.weaponHandSlot.DrawWeapon();
        rightWeaponEquippedSlot.attacker.InitWeapon(rightWeaponEquippedSlot.weapon);
    }

    public void DrawBackWeapon()
    {
        backWeaponEquippedSlot.weapon.isInHand = true;
        backWeaponEquippedSlot.weaponHandSlot.DrawWeapon();
        backWeaponEquippedSlot.attacker.InitWeapon(backWeaponEquippedSlot.weapon);
    }
    #endregion

    private void CheckHands(Weapon weapon)
    {
        if(weapon.isTwoHanded)
        {
            if(rightWeaponEquippedSlot.weaponHandSlot.currentWeaponModel != null)
                rightWeaponEquippedSlot.weaponHandSlot.SheathWeapon();
            if (leftWeaponEquippedSlot.weaponHandSlot.currentWeaponModel != null)
                leftWeaponEquippedSlot.weaponHandSlot.SheathWeapon();
        }
    }
}
