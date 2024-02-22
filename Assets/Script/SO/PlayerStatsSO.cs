using UnityEngine;

[CreateAssetMenu(menuName = "Jelly/stats")]
public class PlayerStatsSO : ScriptableObject
{
    public float MaxHealth = 100;
    public float MaxStamina = 100;
    public float StaminaNeedToAttack = 5;
    public float StaminaNeedToJump = 1;
    public float StaminaNeedToDash = 3;
    public float StaminaMultiplier = 2;

    [Space(5)]
    public float StaminaCoolDownTime = 3;
    public float StaminaRefilSpeed = 5;

    [Space(5)]
    public float SpecialAttackACooldownTime = 4;
    public float SpecialAttackARechargeSpeed = 5;
    public float SpecialAttack_A_EnergyLevel = 10;
    public float SpecialAttack_B_EnergyLevel = 10;
}

[CreateAssetMenu(menuName = "Jelly/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float MaxHealth = 100;
    public float ChaseRadius = 25;
    public float AttackRadius = 10;
}
