using System.Collections;
using UnityEngine;

public class FireworkController : MonoBehaviour
{
    int fireworkCount = 0;
    bool fireworksOngoing = true;
    int randomFireworksThrown;
    [SerializeField] KeyCode countKeyCode;
    void Start()
    {
        StartCoroutine(ThrowFireworks());
    }
    void Update()
    {
        if (fireworksOngoing) CountFireworks();
    }
    void CountFireworks()
    {
        if (Input.GetKeyDown(countKeyCode))
        {
            fireworkCount++;
        }
    }
    public int CloserToCount()
    {
        int countDiff = Mathf.Abs(fireworkCount - randomFireworksThrown);
        return countDiff;
    }
    IEnumerator ThrowFireworks()
    {
        randomFireworksThrown = Random.Range(15, 25);
        yield return new WaitForSecondsRealtime(2);
        fireworksOngoing = true;
        for (int i = 0; i < randomFireworksThrown; i++)
        {

        }
        yield return new WaitForSecondsRealtime(3);
        fireworksOngoing = false;
    }
    public bool AreFireworksOngoing()
    {
        return fireworksOngoing;
    }
}
