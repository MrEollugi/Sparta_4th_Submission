using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemData[] slots = new ItemData[2];
    private int selectedIndex = 0;

    public void Pickup(ItemData item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                UpdateUI();
                return;
            }
        }
    }

    public void UseCurrentItem()
    {
        if (slots[selectedIndex] != null)
        {
            slots[selectedIndex] = null;
            UpdateUI();
        }
    }

    public void DropCurrentItem()
    {
        if(slots[selectedIndex] != null)
        {
            slots[selectedIndex] = null;
            UpdateUI();
        }
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
