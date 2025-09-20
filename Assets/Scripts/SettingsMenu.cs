using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void OnReturnClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
