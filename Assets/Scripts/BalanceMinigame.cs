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
    [SerializeField] float negRandomAngVel1 = -40;
    [SerializeField] float posRandomAngVel1 = 40;
    [SerializeField] float negRandomAngVel2 = -40;
    [SerializeField] float posRandomAngVel2 = 40;
    [SerializeField] float raceTime = 20;
    public bool canControl_P1 = true;
    public bool canControl_P2 = true;
    public override void StartMinigame()
    {
        timerScript = new Timer(raceTime, Timer.TimerReset.Manual);
        timerScript.OnTimerDone += EndRace;
        StartCoroutine(RandomInbalance(personRigidbody1, 1, negRandomAngVel1, posRandomAngVel1));
        StartCoroutine(RandomInbalance(personRigidbody2, 2, negRandomAngVel2, posRandomAngVel2));

        HasStarted = true;
    }
    public override void ResetMinigame()
    {
        
    }
    void Update()
    {
        if (!HasStarted) return;

        if (canControl_P1)
        {
            BalanceSelf(personRigidbody1, "HorizontalPlayer1");
        }

        if (canControl_P2)
        {
            BalanceSelf(personRigidbody2, "HorizontalPlayer2");
        }
        
        timerScript.CountTimer();
    }
    IEnumerator RandomInbalance(Rigidbody2D givenRigidbody, int player, float givenNegRandAngVel, float givenPosRandAngVel)
    {
        float randomAngularVelocity;
        bool canControl = true;

        while (true)
        {
            if (player == 1) canControl = canControl_P1;
            else canControl = canControl_P2;

            randomTime = Random.Range(minRandomTime, maxRandomTime);

            yield return new WaitForSecondsRealtime(randomTime);

            randomAngularVelocity = Random.Range(givenNegRandAngVel, givenPosRandAngVel);

            if (canControl) givenRigidbody.angularVelocity = randomAngularVelocity;
        }
    }
    void BalanceSelf(Rigidbody2D givenRigidbody, string playerAxis)
    {
        float balanceAmount = balanceSpeed * Input.GetAxis(playerAxis);
        givenRigidbody.angularVelocity += balanceAmount;
    }
    void EndRace()
    {
        MinigameEnd();
    }
}
