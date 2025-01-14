
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private Item itemData;
    [SerializeField] private int stackSize;

    public Item ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(Item source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(Item data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.maxAmount - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if(stackSize + amountToAdd <= itemData.maxAmount)
            return true;
        else 
            return false;
    }

    public void AddToStack(int amount)
    {
        Debug.Log($"Add to stack");
        stackSize += amount;
    }

    public void RemoveFromStack(int amount) 
    {
        stackSize -= amount;
    }
}
