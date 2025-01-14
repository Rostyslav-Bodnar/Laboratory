using UnityEditor.Animations;
using UnityEngine;

public abstract class EnemySO : ScriptableObject
{
    public string Name;
    public GameObject prefab;
    public Animator animator;
    public AnimatorController animatorController;

    [Header("EnemyStats")]
    public float health;
    public float power;
    public float speed;
    public float detectionRadius;
    public float detectionSpeed;
    public float attackRadius;
    public float maxViewableAngle;
    public float minViewableAngle;

    [Header("Combos")]
    public string[] nameOfCombos;
}
