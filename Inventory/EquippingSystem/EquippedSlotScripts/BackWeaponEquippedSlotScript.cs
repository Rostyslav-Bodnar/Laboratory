using UnityEngine;

public class BackWeaponEquippedSlotScript : HandedItemEquippedSlotScript
{
    [SerializeField] public Weapon weapon;
    public override void EquipItem(Item addedItem)
    {
        if (weapon != null)
            UnEquipItem();
        base.EquipItem(addedItem);
        weapon = addedItem as Weapon;
        itemImage.sprite = weapon.icon;
        if (weapon != null)
        {
            weaponHandSlot.LoadWeaponOnSlot(weapon);
        }
    }

    public override bool CanEquipItem(Item item)
    {
        return item is DistanceWeapon;
    }

    public override void SortInventory()
    {
        base.SortInventory();
        _eventBus.Invoke(new SortInventorySignal(typeof(DistanceWeapon)));
    }
}
