using UnityEngine;
[CreateAssetMenu(menuName = "Jelly/EnemyStatsSO")]
public class EnemyStatsSO : ScriptableObject
{
    public float MaxHealth = 100;
    public float ChaseRadius = 25;
    public float AttackRadius = 10;
}
