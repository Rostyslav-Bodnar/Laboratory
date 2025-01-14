using System.Collections.Generic;
using UnityEngine;

public class StaticEquippableInventoryDisplay : InventoryDisplay<UI_InventoryEquippedSlot>
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private UI_InventoryEquippedSlot[] equippedSlots;

    private SortingInventorySystem sortingInventorySystem;

    protected override void Start()
    {
        base.Start();
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.EquippableInventorySystem;
        }
        sortingInventorySystem = new SortingInventorySystem(inventoryHolder, this);
        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<UI_InventoryEquippedSlot, InventorySlot>();

        if (equippedSlots.Length != inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject}: {equippedSlots.Length}, {inventorySystem.InventorySize}");

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(equippedSlots[i], inventorySystem.InventorySlots[i]);
            equippedSlots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }



}
