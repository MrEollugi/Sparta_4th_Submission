using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    #region UI References

    public InspectUIManager Inspect;
    public InventoryUI Inventory;
    public HealthBarUI HealthBar;
    public StaminaBarUI StaminaBar;
    public PauseMenuUI PauseMenu;

    #endregion

    #region Pause Menu Control
    public void ShowPauseMenu()
    {
        PauseMenu.Show();
    }
    public void HidePauseMenu()
    {
        PauseMenu.Hide();
    }
    #endregion
}
