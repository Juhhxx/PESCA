using System.Collections;
using UnityEngine;

public class BalanceMinigame : MiniGame
{
    [SerializeField] Rigidbody2D personRigidbody1;
    [SerializeField] Rigidbody2D personRigidbody2;
    Timer timerScript;
    float randomTime;
    [SerializeField] float balanceSpeed;
    [SerializeField] float minRandomTime = 0.5f;
    [SerializeField] float maxRandomTime = 1.5f;
    [SerializeField] float negRandomAngVel = -40;
    [SerializeField] float posRandomAngVel = 40;
    [SerializeField] float raceTime = 20;
    void Start()
    {
        timerScript = new Timer(raceTime, Timer.TimerReset.Manual);
        // timerScript.OnTimerDone += 
        StartCoroutine(RandomInbalance(personRigidbody1));
        StartCoroutine(RandomInbalance(personRigidbody2));
    }
    void Update()
    {
        BalanceSelf(personRigidbody1, "HorizontalPlayer1");
        BalanceSelf(personRigidbody2, "HorizontalPlayer2");
    }
    IEnumerator RandomInbalance(Rigidbody2D givenRigidbody)
    {
        float randomAngularVelocity;
        while (true)
        {
            randomTime = Random.Range(minRandomTime, maxRandomTime);
            yield return new WaitForSecondsRealtime(randomTime);
            randomAngularVelocity = Random.Range(negRandomAngVel, posRandomAngVel);
            givenRigidbody.angularVelocity = randomAngularVelocity;
        }
    }
    void BalanceSelf(Rigidbody2D givenRigidbody, string playerAxis)
    {
        float balanceAmount = balanceSpeed * Input.GetAxis(playerAxis);
        givenRigidbody.angularVelocity += balanceAmount; 
    }
    public override void StartMinigame()
    {

    }

    public override void ResetMinigame()
    {

    }
}
