using System.Collections;
using TMPro;
using UnityEngine;

public class FireworkController : MonoBehaviour
{
    int fireworkCount = 0;
    bool fireworksOngoing = true;
    int randomFireworksThrown;
    [SerializeField] TextMeshProUGUI counterText;
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
            if (fireworkCount < 10)
            {
                counterText.text = "0" + fireworkCount.ToString();
                counterText.GetComponent<TMPPopWave>().PlayPop();
            }
            else
            {
                counterText.text = fireworkCount.ToString();
                counterText.GetComponent<TMPPopWave>().PlayPop();
            }

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
            yield return new WaitForSecondsRealtime(Random.Range(.5f, 1.5f));
            //Instanciate here firework prefab
        }
        yield return new WaitForSecondsRealtime(3);
        fireworksOngoing = false;
    }
    public bool AreFireworksOngoing()
    {
        return fireworksOngoing;
    }
    
}
