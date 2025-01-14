using UnityEngine;

[CreateAssetMenu(menuName = "A.I./Enemy Action/Attack Action")]
public class EnemyAttackActionSO : EnemyActionSO
{
    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maxAttackAngle = 35;
    public float minAttackAngle = -35;

    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;


}
