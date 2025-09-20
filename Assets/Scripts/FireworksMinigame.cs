using System.Collections;
using UnityEngine;

public class FireworksMinigame : MiniGame
{
    bool playerOneWins = false;
    bool playerTwoWins = false;
    [SerializeField] FireworkController fireworkControllerScript1;
    [SerializeField] FireworkController fireworkControllerScript2;
    public override void StartMinigame()
    {

    }

    public override void ResetMinigame()
    {

    }
    void Update()
    {
        if (!fireworkControllerScript1.AreFireworksOngoing() && !fireworkControllerScript2.AreFireworksOngoing()) CompareCounts();
    }
    void CompareCounts()
    {
        int countPlayer1 = fireworkControllerScript1.CloserToCount();
        int countPlayer2 = fireworkControllerScript2.CloserToCount();
        if (countPlayer1 < countPlayer2) playerOneWins = true;
        else playerTwoWins = true;
        MinigameEnd();
    }
}
