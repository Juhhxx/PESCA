using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
