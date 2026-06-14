using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject exitPanel;

    public void ShowExitConfirmation()
    {
        exitPanel.SetActive(true);
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}