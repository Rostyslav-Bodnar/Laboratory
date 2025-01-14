using UnityEngine;

public class BlockingCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider blockingCollider;

    public float blockingPhysicalDamageAbsorption;

    private EventBus _eventBus;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        blockingCollider = GetComponent<BoxCollider>();
        blockingCollider.gameObject.SetActive(true);
        blockingCollider.isTrigger = true;
        blockingCollider.enabled = false;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    public void SetColliderDamageAbsorbtion(Weapon blockingWeapon)
    {
        if(blockingWeapon != null)
        {
            blockingPhysicalDamageAbsorption = blockingWeapon.damageAbsorption;
        }
    }

    public void EnableBlockingCollider()
    {
        blockingCollider.enabled = true;
    }

    public void DisableBlockingCollider()
    {
        blockingCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Weapon")
        {
            Weapon weapon = collision.GetComponent<Weapon>();

            float damage = weapon.damage - blockingPhysicalDamageAbsorption;
            _eventBus.Invoke(new OnHealthChanged(damage));
        }
    }
}
