using System.Collections.Generic;
using UnityEngine;

public class StaticNonEquippableInventoryDisplay : InventoryDisplay<UI_InventorySlot>
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private UI_InventorySlot[] slots;

    protected override void Start()
    {
        base.Start();
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.NonEquippableInventorySystem; // Виправлено на EquippableInventorySystem
            //_eventBus.Subscribe<OnInventorySlotChanged>(UpdateSlotSignal);
        }
        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<UI_InventorySlot, InventorySlot>();

        if (slots.Length != inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
