using UnityEngine;

public class ArmorEquippedSlotScript : EquippedSlotScript
{
    [SerializeField] private Armor armor;
    [SerializeField] private ArmorSlot armorSlot;
    [SerializeField] private ArmorType armorType;

    public override void EquipItem(Item equippedItem)
    {
        base.EquipItem(equippedItem);
        armor = (Armor)equippedItem;

        if(armorSlot != null)
        {
            armorSlot.LoadArmorOnSlot(armor);
        }
        
    }

    public override bool CanEquipItem(Item item)
    {
        return item is Armor;
    }

    public override void SortInventory()
    {
        base.SortInventory();
        Debug.Log($"Sort {this.GetType()}");
        _eventBus.Invoke(new SortInventorySignal(typeof(Armor)));
        isActive = true;
    }
}
