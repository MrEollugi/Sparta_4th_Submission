using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;
    public GameObject itemPrefab;

    public List<ItemEffect> effects;
}
