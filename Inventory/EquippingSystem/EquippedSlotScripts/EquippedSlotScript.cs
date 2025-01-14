using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public abstract class EquippedSlotScript : MonoBehaviour
{
    [SerializeField] protected Image itemImage;
    [SerializeField] private Button button;

    public bool isActive;

    protected EventBus _eventBus;

    private void Start()
    {
        button = GetComponent<Button>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        button.onClick.AddListener(SortInventory);
    }

    public virtual void EquipItem(Item addedItem)
    {
        itemImage.color = new Color(1, 1, 1, 1);
        Debug.Log("Equip");
    }

    public virtual void UnEquipItem()
    {
        itemImage.sprite = null;
        itemImage.color = new Color(0, 0, 0, 0);
    }

    public abstract bool CanEquipItem(Item item);


    public virtual void SortInventory()
    {
        _eventBus.Invoke(new DeactiveSlotsSignal());
    }


}
