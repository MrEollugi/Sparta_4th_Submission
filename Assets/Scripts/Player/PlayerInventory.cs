using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Inventory Data

    // 마리오 카트의 아이템 인벤토리를 생각하시면 이해가 편할 것 같습니다.
    // 인벤토리 슬롯(2칸) 
    private ItemData[] slots = new ItemData[2];
    // 현재 선택된 인벤토리 슬롯의 인덱스
    private int selectedIndex = 0;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        selectedIndex = 0;
        UpdateUI();
    }

    #endregion

    #region Public Methods

    #region 인벤토리에 아이템 추가
    // 아이템을 인벤토리에 추가
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
    #endregion

    #region 아이템 사용
    // 현재 선택된 슬롯의 아이템을 사용
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
    #endregion

    #region 아이템 버리기
    // 현재 선택된 슬롯의 아이템을 버리기
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
    #endregion

    #region 선택 슬롯 변경
    // 선택 슬롯 변경
    public void SwitchSlot()
    {
        selectedIndex = 1 - selectedIndex;
        UpdateUI();
    }
    #endregion

    #endregion

    #region UI Update

    private void UpdateUI()
    {
        InventoryUI.Instance.SetSlots(slots, selectedIndex);
    }

    #endregion
}
