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

    // 인벤토리 슬롯 UI를 업데이트
    // slots는 현재 슬롯에 들어있는 아이템 배열
    // selectedIndex는 현재 선택된 슬롯의 인덱스
    public void SetSlots(ItemData[] slots, int selectedIndex)
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            bool hasItem = slots[i] != null;

            // 아이템 아이콘 처리
            if (hasItem)
            {
                slotIcons[i].sprite = slots[i].icon;
                slotIcons[i].enabled = true;

                // 선택된 슬롯은 밝게, 나머지는 반투명하게 처리
                slotIcons[i].color = (i == selectedIndex)
                    ? Color.white
                    : new Color(0.6f, 0.6f, 0.6f, 0.5f);
            }
            else
            {
                slotIcons[i].enabled = false;
            }

            // 슬롯 선택 테두리 처리
            selectors[i].SetActive(true);
            selectors[i].GetComponent<Image>().color = (i == selectedIndex)
                ? Color.white
                : new Color(0.4f, 0.4f, 0.4f, 0.6f);
        }
    }

    #endregion
}
