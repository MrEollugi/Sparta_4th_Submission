using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemData[] slots = new ItemData[2];
    private int selectedIndex = 0;

    private void Start()
    {
        selectedIndex = 0;
        UpdateUI();
    }

    public bool Pickup(ItemData item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                Debug.Log($"[Inventory] Picked up item: {item.itemName}");
                UpdateUI();
                return true;
            }
        }

        return false;
    }

    public void UseCurrentItem()
    {
        var item = slots[selectedIndex];
        if (item == null) return;

        foreach (var effect in item.effects)
        {
            effect.ApplyEffect(Player.Instance);
        }

        slots[selectedIndex] = null;
        UpdateUI();
    }

    public void DropCurrentItem()
    {
        var item = slots[selectedIndex];
        if (item == null) return;

        Vector3 dropPos = Player.Instance.transform.position + Player.Instance.transform.forward * 1.5f;
        dropPos.y = 1f;
        if (item.itemPrefab != null)
        {
            GameObject dropped = Instantiate(item.itemPrefab, dropPos, Quaternion.identity);
        }

        slots[selectedIndex] = null;
        UpdateUI();
    }

    public void SwitchSlot()
    {
        selectedIndex = 1 - selectedIndex;
        UpdateUI();
    }

    private void UpdateUI()
    {
        InventoryUI.Instance.SetSlots(slots, selectedIndex);
    }
}
