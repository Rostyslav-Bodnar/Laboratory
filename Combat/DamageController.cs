using UnityEngine;

public class DamageController
{
    public float damage = 10f;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask npcLayer;

    private EventBus _eventBus;

    public DamageController()
    {
        Init();
    }

    private void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }

    private void Subscribe()
    {
        //_eventBus.Subscribe<OnPlayerAttack>(Attack);
    }

    public void Attack(OnPlayerAttack signal)
    {
        RaycastHit hit;
        Vector3 direction = attackPoint.forward;

        if (Physics.Raycast(attackPoint.position, direction, out hit, attackRange, npcLayer))
        {
            Debug.Log("Damage");
        }
    }
}
