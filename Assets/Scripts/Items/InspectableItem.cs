using UnityEngine;

[RequireComponent (typeof(Collider))]
public class InspectableItem : MonoBehaviour
{
    public ItemData itemData;

    public string title => itemData.itemName;
    public string description => itemData.description;
    public Sprite icon => itemData.icon;
}
