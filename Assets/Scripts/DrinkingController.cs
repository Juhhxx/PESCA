using NUnit.Framework;
using UnityEngine;

public class DrinkingController : MonoBehaviour
{
    [SerializeField] KeyCode getDrinkKeyCode;
    [SerializeField] KeyCode drinkAllKeyCode;
    [SerializeField] int getDrinkClicks = 1;
    int hasClickedGetDrink = 0;
    int hasClickedDrinkAll = 0;
    [SerializeField] int drinkAllClicks = 5;
    [SerializeField] DrinkingMinigame drinkingMinigameScript;
    int drinksTaken = 0;
    bool noDrinkPresent = true;
    void Update()
    {
        if (drinkingMinigameScript.IsGameOngoing())
        {
            if (noDrinkPresent)
            {
                // Instantiate(Boobs);
            }
            else
            {
                GetDrink();
            }
        }
    }
    void GetDrink()
    {
        if (getDrinkClicks == hasClickedGetDrink)
        {
            Debug.Log("Drink obtained!");
            DrinkItAll();
            return;
        }
        if (Input.GetKeyDown(getDrinkKeyCode))
        {
            hasClickedGetDrink++;
        }
    }
    void DrinkItAll()
    {
        if (Input.GetKeyDown(drinkAllKeyCode))
        {
            hasClickedDrinkAll++;
        }
        if (drinkAllClicks == hasClickedDrinkAll)
        {
            Debug.Log("Drink finished!");
            noDrinkPresent = true;
            drinksTaken++;
            hasClickedDrinkAll = 0;
            hasClickedGetDrink = 0;
        }
    }
    public int HowManyDrinks()
    {
        return drinksTaken;
    }
}
