using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    
    [SerializeField] protected InventorySystem nonEquippableInventorySystem;
    [SerializeField] protected InventorySystem equippableInventorySystem;

    public InventorySystem NonEquippableInventorySystem => nonEquippableInventorySystem;
    public InventorySystem EquippableInventorySystem => equippableInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        nonEquippableInventorySystem = new InventorySystem(inventorySize);
        equippableInventorySystem = new InventorySystem(inventorySize);
    }

    public bool AddItemToInventory(Item item, int amount)
    {
        if (item is IEquippable)
        {
            return equippableInventorySystem.AddToInventory(item, amount);
        }
        else
        {
            return nonEquippableInventorySystem.AddToInventory(item, amount);
        }
    }
}
