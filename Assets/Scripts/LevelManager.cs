using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourDDOL<LevelManager>
{
    [SerializeField][Scene] private string _mainMenuScene;
    [SerializeField][Expandable] private LevelList _levels;
    private LevelProfile _currentLevel;
    private Queue<LevelProfile> _levelQueue;
    private MiniGame _currentMinigame;
    private DialogueRunner _currentDialogueRunner;
    private Animator _anim;

    private void Awake()
    {
        SingletonCheck(this);
    }

    private void Start()
    {
        _levelQueue = new Queue<LevelProfile>(_levels.LevelOrder);
        _anim = GetComponent<Animator>();

        SetUpEvents();
        _anim.SetTrigger("FadeIn");

        SceneManager.sceneLoaded += (Scene s, LoadSceneMode m) => SetUpEvents();
        SceneManager.sceneUnloaded += (Scene s) => TurnOffEvents();
    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public void GoToNextLevel()
    {
        if (_levelQueue.Count == 0)
        {
            _levelQueue = new Queue<LevelProfile>(_levels.LevelOrder);
            SceneManager.LoadScene(_mainMenuScene);
            return;
        }

        float waitTime = 0;

        if (_currentLevel != null) waitTime = _currentLevel.NextSceneDelay;

        LevelProfile level = _levelQueue.Dequeue();


        Debug.Log($"GOING TO LEVEL {level}");
        StartCoroutine(LoadLevelCR(waitTime, level));
    }

    private IEnumerator LoadLevelCR(float waitTime, LevelProfile level)
    {
        yield return new WaitForSeconds(waitTime);
        
        _anim.SetTrigger("FadeOut");
        yield return new WaitUntil(() => !(_anim.GetCurrentAnimatorStateInfo(0).length >
        _anim.GetCurrentAnimatorStateInfo(0).normalizedTime));
        
        _ = LoadLevel(level);
    }
    private async Task LoadLevel(LevelProfile level)
    {
        _currentLevel = level;

        Debug.Log($"LOADING LEVEL {level}");

        await SceneManager.LoadSceneAsync(level.LevelScene);

        Debug.Log($"LOADED LEVEL {level}");

        if (level.StartMinigame) StartCoroutine(StartLevelCR(level.StartDelay));
        _currentDialogueRunner?.PlayDialogue();
        _anim.SetTrigger("FadeIn");
    }

    private IEnumerator StartLevelCR(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _currentMinigame?.StartMinigame();
        Debug.Log($"Started {_currentMinigame.name}");
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
        _currentMinigame = FindAnyObjectByType<MiniGame>();
        _currentDialogueRunner = FindAnyObjectByType<DialogueRunner>();
        DialogueShower.Instance.StopAllCoroutines();
        DialogueShower.Instance.ClearDialogues();
        ScreenShakeManager.Instance.StopAllCoroutines();

        if (_currentMinigame != null)
        {
            _currentMinigame.OnMinigameEnd += GoToNextLevel;
            Debug.Log($"Subscribed to OnMingameEnd in {_currentMinigame.name}");
        }
    }
    private void TurnOffEvents()
    {
        if (_currentMinigame != null)
        {
            _currentMinigame.OnMinigameEnd -= GoToNextLevel;
        }
    }
}
