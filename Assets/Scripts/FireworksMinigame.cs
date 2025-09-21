using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FireworksMinigame : MiniGame
{
    bool playerOneWins = false;
    bool playerTwoWins = false;
    [SerializeField] FireworkController fireworkControllerScript1;
    [SerializeField] FireworkController fireworkControllerScript2;
    public UnityEvent OnFireworksStart;
    public bool _didCount = false;

    public void Start()
    {
        fireworkControllerScript1.gameObject.SetActive(false);
        fireworkControllerScript2.gameObject.SetActive(false);
    }
    public override void StartMinigame()
    {
        HasStarted = true;
        fireworkControllerScript1.gameObject.SetActive(true);
        fireworkControllerScript2.gameObject.SetActive(true);

        OnFireworksStart.Invoke();
    }

    public override void ResetMinigame()
    {

    }
    void Update()
    {
        if (!HasStarted) return;
        if (!fireworkControllerScript1.AreFireworksOngoing() && !fireworkControllerScript2.AreFireworksOngoing()) CompareCounts();
    }
    void CompareCounts()
    {
        if (_didCount) return;
        
        int countPlayer1 = fireworkControllerScript1.CloserToCount();
        int countPlayer2 = fireworkControllerScript2.CloserToCount();
        if (countPlayer1 < countPlayer2) playerOneWins = true;
        else playerTwoWins = true;

        _didCount = true;

        MinigameEnd();
    }
}
