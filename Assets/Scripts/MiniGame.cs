using System;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public event Action OnMinigameEnd;

    public abstract void StartMinigame();
    public abstract void ResetMinigame();

    public void MinigameEnd()
    {
        OnMinigameEnd?.Invoke();
    }
}
