using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : UI_Slot
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay<UI_InventorySlot> ParentDisplay { get; private set; }


    private void Awake()
    {
        ClearSlot();

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay<UI_InventorySlot>>();
    }
    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public override void UpdateUISlot(InventorySlot slot)
    {

        if(slot.ItemData != null)
        {
            itemImage.sprite = slot.ItemData.icon;
            itemImage.color = new Color(1, 1, 1, 1);
            if (slot.StackSize > 1) itemAmount.text = slot.StackSize.ToString();
            else itemAmount.text = "";
        }
        else
        {
            ClearSlot();
        }
        
    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }

    public override void ClearSlot()
    {
        assignedInventorySlot = null;
        itemImage.sprite = null;
        itemImage.color = new Color(0, 0, 0, 0);
        itemAmount.text = "";
    }
}
