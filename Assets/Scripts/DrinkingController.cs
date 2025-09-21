using UnityEngine;
using System.Collections.Generic;
using System.Collections;
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
    Rigidbody2D drinkRigidbody;
    private List<GameObject> squishTargets = new List<GameObject>();
    int drinksTaken = 0;
    bool noDrinkPresent = true;
    GameObject currentDrink;
    [SerializeField] Vector2 drinkThrowForce;
    [SerializeField] float drinkRotationForce;
    bool canClick = true;
    [SerializeField] Transform drinkSpawnpoint;
    [SerializeField] float drinkSlideSpeed;

    void Update()
    {
        Debug.Log($"{hasClickedGetDrink}, {hasClickedDrinkAll}");
        if (drinkingMinigameScript.IsGameOngoing())
        {
            if (noDrinkPresent)
            {
                canClick = true;
                noDrinkPresent = false;
                currentDrink = Instantiate(drinkObject, drinkSpawnpoint.position, Quaternion.identity);
                drinkRigidbody = currentDrink.GetComponent<Rigidbody2D>();
                StartCoroutine(SlideDrink(currentDrink.transform.position.x, transform.position.x));
            }
            else
            {
                GetDrink();
            }
        }
    }
    void GetDrink()
    {
        if (Input.GetKeyDown(getDrinkKeyCode) && canClick)
        {
            Debug.Log("Check succesful");
            hasClickedGetDrink += 1;
        }
        if (getDrinkClicks <= hasClickedGetDrink)
        {
            Debug.Log("Drink obtained!");
            DrinkItAll();
        }
    }
    void DrinkItAll()
    {
        if (Input.GetKeyDown(drinkAllKeyCode) && canClick)
        {
            hasClickedDrinkAll++;
            ScreenShakeManager.Instance.Shake(0.3f, 0.15f);
            Debug.Log(drinkAllClicks);
        }
        if (drinkAllClicks == hasClickedDrinkAll)
        {
            currentDrink.GetComponentInChildren<SpriteRenderer>().sprite = emptySprite;
            StartCoroutine(ThrowCan(drinkRigidbody));
            Debug.Log("Drink finished!");
            drinksTaken++;
            canClick = false;
        }
    }
    IEnumerator ThrowCan(Rigidbody2D rb)
    {
        rb.gravityScale = 1;
        rb.AddTorque(drinkRotationForce);
        rb.AddForce(drinkThrowForce);
        yield return new WaitForSecondsRealtime(0.3f);
        Destroy(rb.gameObject);
        noDrinkPresent = true;
        hasClickedDrinkAll = 0;
        hasClickedGetDrink = 0;
    }
    public int HowManyDrinks()
    {
        return drinksTaken;
    }
    private IEnumerator SlideDrink(float currentPosition, float pos)
    {
        float newPos = currentPosition;
        float i = 0;

        while (newPos != pos)
        {
            newPos = Mathf.Lerp(currentPosition, pos,i);

            currentDrink.transform.localPosition = new Vector3(newPos, currentDrink.transform.position.y, 0f);

            i += drinkSlideSpeed * Time.deltaTime;

            yield return null;
        }
    }
}
