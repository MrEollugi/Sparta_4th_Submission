
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Game State

    public bool IsPaused { get; private set; }

    // ���� �Ͻ�����
    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.Instance.ShowPauseMenu();
    }

    // ���� �簳
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.Instance.HidePauseMenu();
    }

    // ���� ����
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // ���� ����� (���׷� ���� �̻�� ��)
    public void RestartGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    #endregion
}
