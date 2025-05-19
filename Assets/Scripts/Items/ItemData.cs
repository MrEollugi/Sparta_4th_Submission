using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;
    public ItemType itemType;

    public float healAmount;
    public float speedBoostAmount;
    public float duration;
}
