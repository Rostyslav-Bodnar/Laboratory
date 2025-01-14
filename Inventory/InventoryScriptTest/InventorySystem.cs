using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    private EventBus _eventBus;

    public InventorySystem(int size)
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++) 
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(Item itemToAdd, int amountToAdd)
    {
        Debug.Log($"{itemToAdd.name}");
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            Debug.Log($"Contains item");
            foreach (var slot in invSlot)
            {
                if(slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    _eventBus.Invoke(new OnInventorySlotChanged(slot));
                    return true;
                }
            }
        }
        if(HasFreeSlot(out InventorySlot freeSlot))
        {
            Debug.Log($"Has free slots");

            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            _eventBus.Invoke(new OnInventorySlotChanged(freeSlot));
            return true;
        }
        return false;   
    }

    public bool ContainsItem(Item itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();

        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
