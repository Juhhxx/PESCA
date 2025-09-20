using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettingsClicked()
    {
        SceneManager.LoadScene("Settings");
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
