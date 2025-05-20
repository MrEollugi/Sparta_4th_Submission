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
        for(int i = 0; i<2; i++)
        {
            slotIcons[i].sprite = slots[i]?.icon;
            slotIcons[i].enabled = slots[i] != null;
            selectors[i].SetActive(i == selectedIndex);
        }
    }
}
