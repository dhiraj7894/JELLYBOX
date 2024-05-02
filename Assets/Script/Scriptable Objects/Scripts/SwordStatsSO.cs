using UnityEngine;

[CreateAssetMenu(menuName = "Jelly/WeaponStatsSO")]
public class SwordStatsSO : ScriptableObject
{
    public GameObject Weapon;
    public string Name;
    public string Description;
    public Sprite UI;

    [Space(10)]
    public float Damage;
    public float DamageMultiplyer;
}
