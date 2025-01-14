using UnityEngine;

public class WeaponEquippedSlotManager : EquippedSlotManager
{
    [SerializeField] private LeftHandedItemEquippedSlotScript leftWeaponEquippedSlotScript;
    [SerializeField] private RightHandedItemEquippedSlotScript rightWeaponEquippedSlotScript;
    [SerializeField] private BackWeaponEquippedSlotScript backWeaponEquippedSlotScript;

    protected override void Start()
    {
        base.Start();
        foreach(var slots in equippedSlots)
        {
            if(slots is HandedItemEquippedSlotScript weaponSlot)
            {
                if (weaponSlot is LeftHandedItemEquippedSlotScript leftSlot)
                    leftWeaponEquippedSlotScript = leftSlot;
                else if (weaponSlot is RightHandedItemEquippedSlotScript rightSlot)
                    rightWeaponEquippedSlotScript = rightSlot;
                else if (weaponSlot is BackWeaponEquippedSlotScript backSlot)
                    backWeaponEquippedSlotScript = backSlot;
            }
        }
    }

    public override void EquipItem(Item item)
    {
        if (item is Weapon weapon)
        {
            if (weapon is MeleeWeapon melee)
            {
                if (rightWeaponEquippedSlotScript.isActive)
                    EquipRightWeapon(melee);
            }
            else if (weapon is Shield shield)
            {
                if (leftWeaponEquippedSlotScript.isActive)
                    EquipLeftWeapon(shield);
            }
            else if(weapon is DistanceWeapon distanceWeapon)
            {
                if (backWeaponEquippedSlotScript.isActive)
                    EquipDistanceWeapon(distanceWeapon);
            }
        }

    }

    private void EquipRightWeapon(MeleeWeapon weapon)
    {

        if (rightWeaponEquippedSlotScript.CanEquipItem(weapon))
        {
            rightWeaponEquippedSlotScript.EquipItem(weapon);
        }
    }

    private void EquipLeftWeapon(Shield shield)
    {
        if (leftWeaponEquippedSlotScript.CanEquipItem(shield))
        {
            leftWeaponEquippedSlotScript.EquipItem(shield);
        }

    }

    private void EquipDistanceWeapon(DistanceWeapon distanceWeapon)
    {
        if(backWeaponEquippedSlotScript.CanEquipItem(distanceWeapon))
        {
            backWeaponEquippedSlotScript.EquipItem(distanceWeapon);
        }
    }

    private void EquipTwoHandedWeapon(Weapon weapon)
    {
        //UnequipAllWeapons();

        backWeaponEquippedSlotScript.EquipItem(weapon);
    }

    private void UnequipAllWeapons()
    {
        //leftWeaponEquippedSlotScript.UnEquipItem();
          rightWeaponEquippedSlotScript.UnEquipItem();

        leftWeaponEquippedSlotScript.weaponHandSlot.SheathWeapon();
        rightWeaponEquippedSlotScript.weaponHandSlot.SheathWeapon();
    }

}

