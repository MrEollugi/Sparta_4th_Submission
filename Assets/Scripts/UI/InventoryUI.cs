using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private Image[] slotIcons;
    [SerializeField] private GameObject[] selectors;

    private void Awake()
    {
        Instance = this;
    }

    public void SetSlots(ItemData[] slots, int selectedIndex)
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            bool hasItem = slots[i] != null;

            if (hasItem)
            {
                slotIcons[i].sprite = slots[i].icon;
                slotIcons[i].enabled = true;

                slotIcons[i].color = (i == selectedIndex)
                    ? Color.white
                    : new Color(0.6f, 0.6f, 0.6f, 0.5f);
            }
            else
            {
                slotIcons[i].enabled = false;
            }

            selectors[i].SetActive(true);
            selectors[i].GetComponent<Image>().color = (i == selectedIndex)
                ? Color.white
                : new Color(0.4f, 0.4f, 0.4f, 0.6f);
        }
    }
}
