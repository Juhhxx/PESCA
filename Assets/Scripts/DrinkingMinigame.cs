using System.Collections;
using UnityEngine;

public class DrinkingMinigame : MiniGame
{
    [SerializeField] DrinkingController drinkingControllerScript1;
    [SerializeField] DrinkingController drinkingControllerScript2;
    bool playerOneWins = false;
    bool playerTwoWins = false;
    bool gameIsOngoing = false;
    [SerializeField] float drinkGameTime = 30;
    public override void StartMinigame()
    {
        StartCoroutine(DrinkGameTime());

        HasStarted = true;
    }
    public override void ResetMinigame()
    {

    }
    IEnumerator DrinkGameTime()
    {
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Game has Started!");
        gameIsOngoing = true;
        yield return new WaitForSecondsRealtime(drinkGameTime);
        gameIsOngoing = false;
        CalculateWinner();
    }
    void CalculateWinner()
    {
        int drinksPlayerOne = drinkingControllerScript1.HowManyDrinks();
        int drinksPlayerTwo = drinkingControllerScript2.HowManyDrinks();
        if (drinksPlayerOne > drinksPlayerTwo) playerOneWins = true;
        if (drinksPlayerOne < drinksPlayerTwo) playerTwoWins = true;

        MinigameEnd();
    }
    public bool IsGameOngoing()
    {
        return gameIsOngoing;
    }
}
