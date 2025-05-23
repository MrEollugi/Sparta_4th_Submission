using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region Singleton

    public static InventoryUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region UI Elements

    [SerializeField] private Image[] slotIcons;
    [SerializeField] private GameObject[] selectors;

    #endregion

    #region UI Update Logic

    // �κ��丮 ���� UI�� ������Ʈ
    // slots�� ���� ���Կ� ����ִ� ������ �迭
    // selectedIndex�� ���� ���õ� ������ �ε���
    public void SetSlots(ItemData[] slots, int selectedIndex)
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            bool hasItem = slots[i] != null;

            // ������ ������ ó��
            if (hasItem)
            {
                slotIcons[i].sprite = slots[i].icon;
                slotIcons[i].enabled = true;

                // ���õ� ������ ���, �������� �������ϰ� ó��
                slotIcons[i].color = (i == selectedIndex)
                    ? Color.white
                    : new Color(0.6f, 0.6f, 0.6f, 0.5f);
            }
            else
            {
                slotIcons[i].enabled = false;
            }

            // ���� ���� �׵θ� ó��
            selectors[i].SetActive(true);
            selectors[i].GetComponent<Image>().color = (i == selectedIndex)
                ? Color.white
                : new Color(0.4f, 0.4f, 0.4f, 0.6f);
        }
    }

    #endregion
}
