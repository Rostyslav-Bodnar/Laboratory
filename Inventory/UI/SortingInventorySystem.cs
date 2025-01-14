using System;
using System.Collections.Generic;
using System.Linq;

public class SortingInventorySystem
{
    private InventoryHolder inventoryHolder;
    private StaticEquippableInventoryDisplay inventoryDisplay;

    private EventBus _eventBus;

    public SortingInventorySystem(InventoryHolder inventoryHolder, StaticEquippableInventoryDisplay inventoryDisplay)
    {
        this.inventoryHolder = inventoryHolder;
        this.inventoryDisplay = inventoryDisplay;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<SortInventorySignal>(SortInventory);
    }

    private void SortInventory(SortInventorySignal signal)
    {
        inventoryDisplay.ClearAllSlots();
        SortInventoryByType(signal.ItemType);
    }

    public void SortInventoryByType(Type ItemType)
    {
        List<InventorySlot> slots = inventoryDisplay.SlotDictionary.Values
                        .Where(slot => slot.ItemData != null && slot.ItemData.GetType() == ItemType)
                        .ToList();

        inventoryDisplay.ClearAllSlots();

        for (int i = 0; i < slots.Count; i++)
        {
            var uiSlot = inventoryDisplay.SlotDictionary.Keys.ElementAt(i); // Отримуємо UI слот за індексом
            uiSlot.UpdateUISlot(slots[i]); // Оновлюємо UI слот відсортованим предметом
        }
    }
}
