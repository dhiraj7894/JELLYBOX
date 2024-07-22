using UnityEngine;


public enum itemType{
    herb,
    potion
}
[CreateAssetMenu(menuName = "Jelly/CollectableItems")]
public class Item : ScriptableObject
{
    public itemType itemType;
    public int value;
    public int count;
    public Sprite icon;
    public string title;
    public string description;
    public GameObject itemObject;
}
