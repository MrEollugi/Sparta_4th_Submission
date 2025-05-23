using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InspectUIManager : MonoBehaviour
{
    #region Singleton

    public static InspectUIManager Instance;

    private void Awake()
    {
        Instance = this;
        // 시작 시 UI 비활성화
        panel.SetActive(false);
    }

    #endregion

    #region UI References

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image iconImage;

    #endregion

    #region Public Methods
    public void Show(ItemData item)
    {
        titleText.text = item.itemName;
        descriptionText.text = item.description;
        iconImage.sprite = item.icon;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
    #endregion
}
