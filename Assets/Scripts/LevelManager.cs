using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourDDOL<LevelManager>
{
    [SerializeField][Expandable] private List<LevelProfile> _levels;
    private LevelProfile _currentLevel;
    private Queue<LevelProfile> _levelQueue;

    private void Awake()
    {
        SingletonCheck(this);
    }

    private void Start()
    {
        _levelQueue = new Queue<LevelProfile>(_levels);

        SetUpEvents();

        SceneManager.sceneLoaded += (Scene s, LoadSceneMode m) => SetUpEvents();
        SceneManager.sceneUnloaded += (Scene s) => TurnOffEvents();

    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void GoToNextLevel()
    {
        if (_levelQueue.Count == 0)
        {
            _levelQueue = new Queue<LevelProfile>(_levels);
            SceneManager.LoadScene("MainMenu");
            return;
        }

        LevelProfile level = _levelQueue.Dequeue();
        Debug.Log($"GOING TO LEVEL {level}");
        _ = LoadLevel(level);
    }
    private async Task LoadLevel(LevelProfile level)
    {
        _currentLevel = level;

        Debug.Log($"LOADING LEVEL {level}");

        await SceneManager.LoadSceneAsync(level.LevelScene);

        Debug.Log($"LOADED LEVEL {level}");
    }
    private IEnumerator RestartLevelCR()
    {
        yield return new WaitForSeconds(1.5f);

        RestartLevel();
    }
    public async void RestartLevel()
    {
        await SceneManager.LoadSceneAsync(_currentLevel.LevelScene);
    }
    private void SetUpEvents()
    {
    }
    private void TurnOffEvents()
    {
    }
}
