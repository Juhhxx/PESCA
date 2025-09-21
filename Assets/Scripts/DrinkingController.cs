using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class DrinkingController : MonoBehaviour
{
    [SerializeField] KeyCode getDrinkKeyCode;
    [SerializeField] KeyCode drinkAllKeyCode;
    [SerializeField] int getDrinkClicks = 1;
    int hasClickedGetDrink = 0;
    int hasClickedDrinkAll = 0;
    [SerializeField] int drinkAllClicks = 5;
    [SerializeField] DrinkingMinigame drinkingMinigameScript;
    [SerializeField] GameObject drinkObject;
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite fullSprite;
    [SerializeField] Transform trash;
    private List<GameObject> squishTargets = new List<GameObject>();
    int drinksTaken = 0;
    bool noDrinkPresent = true;
    GameObject currentDrink;
    Vector3 drinkSpawnPoint;
    private float t;
    private float speed = 1;

    void Update()
    {
        if (drinkingMinigameScript.IsGameOngoing())
        {
            if (noDrinkPresent)
            {
                noDrinkPresent = false;
                // Instantiate(Boobs);
                currentDrink = Instantiate(drinkObject, transform.position + new Vector3(0, 500, 0), Quaternion.identity);
                //squishTargets.Add(currentDrink);
                drinkSpawnPoint = currentDrink.transform.position;
            }
            else
            {
                GetDrink();
            }
        }
        t ++;
    }
    void GetDrink()
    {
        if (getDrinkClicks == hasClickedGetDrink)
        {
            //t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            //currentDrink.transform.position = Vector3.Lerp(drinkSpawnPoint, transform.position, t);
            Debug.Log("Drink obtained!");
            // if (currentDrink.transform.position == transform.position)
            // {
            //     DrinkItAll();
            //     return;
            // }
            currentDrink.transform.position = transform.position;
            DrinkItAll();
            
        }
        if (Input.GetKeyDown(getDrinkKeyCode))
        {
            //instantiate a sprite of a button that has an animation of the button to click to pull a drink
            hasClickedGetDrink++;
        }
    }
    void DrinkItAll()
    {
        if (Input.GetKeyDown(drinkAllKeyCode))
        {
            hasClickedDrinkAll++;
            // foreach (var target in squishTargets)
            // {
            //     if (target != null)
            //         target.GetComponent<SquishEffect>().PlaySquish();
            // }
            Debug.Log(drinkAllClicks);
        }
        if (drinkAllClicks == hasClickedDrinkAll)
        {
            //squishTargets.Remove(currentDrink);
            currentDrink.GetComponent<SpriteRenderer>().sprite = emptySprite;
            //t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            currentDrink.transform.position = Vector3.Lerp(transform.position, trash.position, t);
            
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
