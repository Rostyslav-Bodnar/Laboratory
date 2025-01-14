using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryEquippedSlot : UI_Slot
{
    [SerializeField] private Image icon;
    [SerializeField] private InventorySlot assignedInventorySlot;

    [SerializeField] private Button button;

    private EventBus _eventBus;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay<UI_InventoryEquippedSlot> ParentDisplay { get; private set; }


    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        ClearSlot();

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay<UI_InventoryEquippedSlot>>();
        button.onClick.AddListener(Equip);
    }
    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public override void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData != null)
        {
            assignedInventorySlot = slot;
            icon.sprite = slot.ItemData.icon;
            icon.color = new Color(1, 1, 1, 1);
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
        icon.sprite = null;
        icon.color = new Color(0, 0, 0, 0);
    }

    public void Equip()
    {
        _eventBus.Invoke(new EquipItemSignal(assignedInventorySlot.ItemData));
    }
}
