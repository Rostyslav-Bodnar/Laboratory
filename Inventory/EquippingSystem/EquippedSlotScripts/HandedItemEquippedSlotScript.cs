using UnityEngine;

public abstract class HandedItemEquippedSlotScript : EquippedSlotScript
{
    [SerializeField] public bool isTwoHanded;
    [SerializeField] public WeaponHandSlot weaponHandSlot;
    [SerializeField] public PlayerAttacker attacker;

    public override void EquipItem(Item addedItem)
    {
        base.EquipItem(addedItem);
    }

    public override void UnEquipItem()
    {
        base.UnEquipItem();
        attacker.test = false;
        _eventBus.Invoke(new SetBoolInAnimator("isEnemyVisible", attacker.test));
        weaponHandSlot.UnloadWeaponAndDestroy();
    }

    public override bool CanEquipItem(Item item)
    {
        return item is HandedItem;
    }

    public override void SortInventory()
    {
        base.SortInventory();
        Debug.Log($"Sort {this.GetType()}");
        isActive = true;
    }
}
