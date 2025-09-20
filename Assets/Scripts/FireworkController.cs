using System;
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
    private Vector3 counterPos;
    private float waveOffset = 0;
    void Start()
    {
        counterPos = counterText.transform.position;
        StartCoroutine(ThrowFireworks());
    }
    void Update()
    {
        counterPos.y += MathF.Sin(waveOffset * Mathf.Deg2Rad) * 0.07f;
        if (fireworksOngoing) CountFireworks();

        waveOffset += .5f;

        counterText.transform.position = counterPos;
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
        randomFireworksThrown = UnityEngine.Random.Range(15, 25);
        yield return new WaitForSecondsRealtime(2);
        fireworksOngoing = true;
        for (int i = 0; i < randomFireworksThrown; i++)
        {
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(.5f, 1.5f));
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
