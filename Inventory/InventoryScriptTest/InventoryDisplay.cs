using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class InventoryDisplay<TSlot> : MonoBehaviour where TSlot : UI_Slot
{
    protected InventorySystem inventorySystem;
    public InventorySystem InventorySystem => inventorySystem;

    protected Dictionary<TSlot, InventorySlot> slotDictionary;
    public Dictionary<TSlot, InventorySlot> SlotDictionary => slotDictionary;

    protected EventBus _eventBus;

    protected virtual void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<OnInventorySlotChanged>(UpdateSlotSignal);
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected void UpdateSlotSignal(OnInventorySlotChanged signal)
    {
        UpdateSlot(signal.slot);
    }

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                Debug.Log($"found slot");
                slot.Key.GetComponent<TSlot>().UpdateUISlot(updatedSlot);
            }
        }
    }

    public void ClearAllSlots()
    {
        foreach (var slot in slotDictionary)
        {
            slot.Key.GetComponent<TSlot>().ClearSlot();
        }
    }
}
