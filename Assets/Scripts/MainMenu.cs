using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void OnStartClicked()
    {
        LevelManager.Instance.GoToNextLevel();
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
