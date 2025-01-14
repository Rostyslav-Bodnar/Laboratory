using UnityEngine;

public abstract class EquippedSlotManager : MonoBehaviour
{
    protected EquippedSlotScript[] equippedSlots;

    protected EventBus _eventBus;

    protected virtual void Start()
    {
        Init();
    }
    private void Init()
    {
        equippedSlots = GetComponentsInChildren<EquippedSlotScript>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<EquipItemSignal>(EquipItemSignalFunc);
        _eventBus.Subscribe<DeactiveSlotsSignal>(DeactiveAllSlots);
    }

    private void EquipItemSignalFunc(EquipItemSignal signal)
    {
        EquipItem(signal.item);
    }

    public abstract void EquipItem(Item item);

    protected void DeactiveAllSlots(DeactiveSlotsSignal signal)
    {
        foreach(var slot in equippedSlots)
        {
            slot.isActive = false;
        }
    }
}
