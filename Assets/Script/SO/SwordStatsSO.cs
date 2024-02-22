using UnityEngine;

[CreateAssetMenu(menuName = "Jelly/stats")]
public class SwordStatsSO : ScriptableObject
{
    public GameObject Sword;
    public string Name;
    public string Description;
    public Sprite UI;

    [Space(10)]
    public float Damage;
    public float DamageMultiplyer;
}
