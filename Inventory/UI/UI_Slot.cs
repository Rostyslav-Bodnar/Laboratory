using UnityEngine;

public abstract class UI_Slot : MonoBehaviour
{
    public abstract void ClearSlot();

    public abstract void UpdateUISlot(InventorySlot slot);

}
