using UnityEngine;
public class LeftHandedItemEquippedSlotScript : HandedItemEquippedSlotScript
{

    [SerializeField] public Shield shield;
    public override void EquipItem(Item addedItem)
    {
        if (shield != null)
            UnEquipItem();
        base.EquipItem(addedItem);
        shield = addedItem as Shield;
        itemImage.sprite = shield.icon;
        if (shield != null)
        {
            weaponHandSlot.LoadWeaponOnSlot(shield);
            attacker.InitLeftWeapon(shield);
        }
    }

    public override bool CanEquipItem(Item item)
    {
        return item is Shield;
    }

    public override void SortInventory()
    {
        base.SortInventory();
        _eventBus.Invoke(new SortInventorySignal(typeof(Shield)));
    }
}
