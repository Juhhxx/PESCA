using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProfile", menuName = "Scriptable Objects/LevelProfile")]
public class LevelProfile : ScriptableObject
{
    [SerializeField][Scene] private string _levelScene;
    public string LevelScene => _levelScene;

    [SerializeField][Range(1, 5)] private int _maxShadowsAllowed;
    public int MaxShadowsAllowed => _maxShadowsAllowed;
}
