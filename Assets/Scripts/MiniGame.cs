using System;
using UnityEngine;
using NaughtyAttributes;

public abstract class MiniGame : MonoBehaviour
{
    public event Action OnMinigameEnd;
    public bool HasStarted = false;


    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public abstract void StartMinigame();
    public abstract void ResetMinigame();

    public void MinigameEnd()
    {
        OnMinigameEnd?.Invoke();
    }
}
