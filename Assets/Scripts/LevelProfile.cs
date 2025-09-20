using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProfile", menuName = "Scriptable Objects/LevelProfile")]
public class LevelProfile : ScriptableObject
{
    [SerializeField][Scene] private string _levelScene;
    public string LevelScene => _levelScene;

}
