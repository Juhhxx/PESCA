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
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
