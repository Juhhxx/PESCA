using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProfile", menuName = "Scriptable Objects/LevelProfile")]
public class LevelProfile : ScriptableObject
{
    [SerializeField][Scene] private string _levelScene;
    public string LevelScene => _levelScene;

    [field: SerializeField] public bool StartMinigame { get; private set; }

    [field: AllowNesting]
    [field: ShowIf("StartMinigame")]
    [field: SerializeField] public float StartDelay { get; private set; }
    [field: SerializeField] public float NextSceneDelay { get; private set; }

}
