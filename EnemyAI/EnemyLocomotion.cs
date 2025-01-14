using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    public EnemyAnimatorController enemyAnimatorController;

    public NavMeshAgent navMeshAgent;
    public Rigidbody rb;
    public Animator anim;

    public LayerMask detectionLayer;

    private void Start()
    {
        rb.isKinematic = false;
        navMeshAgent.enabled = false;
        anim = GetComponent<Animator>();
        enemyAnimatorController = GetComponent<EnemyAnimatorController>();
    }
}
