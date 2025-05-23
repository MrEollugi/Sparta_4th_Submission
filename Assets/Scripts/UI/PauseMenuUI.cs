using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    #region UI References

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        panel.SetActive(false);
    }

    #endregion

    #region Public Methods
    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void OnClickResume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnClickRestart()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnClickQuit()
    {
        GameManager.Instance.QuitGame();
    }
    #endregion
}
