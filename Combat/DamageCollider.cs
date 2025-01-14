using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private Collider damageCollider;
    [SerializeField] private Weapon weapon;

    private EventBus _eventBus;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled=false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if(player != null)
            {
                _eventBus.Invoke(new OnHealthChanged(weapon.damage));
            }
        }
        if (collision.tag == "Enemy")
        {
            Debug.Log(weapon.damage);
            EnemyManager enemy = collision.GetComponent<EnemyManager>();
            if (enemy != null)
            {
                Debug.Log("Hit the enemy");
            }
        }
    }
}
