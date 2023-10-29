using UnityEngine;

[CreateAssetMenu(menuName = "Jelly/stats")]
public class Stats : ScriptableObject
{
    public float MaxHealth = 100;
    public float MaxStamina = 100;
    public float StaminaNeedToAttack = 5;
    public float StaminaNeedToJump = 1;
    public float StaminaMultiplier = 2;

    [Space(5)]
    public float StaminaCoolDownTime = 3;
    public float StaminaRefilSpeed = 5;
}