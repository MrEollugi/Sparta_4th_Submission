using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Inventory Data

    // ������ īƮ�� ������ �κ��丮�� �����Ͻø� ���ذ� ���� �� �����ϴ�.
    // �κ��丮 ����(2ĭ) 
    private ItemData[] slots = new ItemData[2];
    // ���� ���õ� �κ��丮 ������ �ε���
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

    #region �κ��丮�� ������ �߰�
    // �������� �κ��丮�� �߰�
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

    #region ������ ���
    // ���� ���õ� ������ �������� ���
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

    #region ������ ������
    // ���� ���õ� ������ �������� ������
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

    #region ���� ���� ����
    // ���� ���� ����
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
