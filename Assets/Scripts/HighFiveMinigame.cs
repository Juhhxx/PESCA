using UnityEngine;
using UnityEngine.Events;

public class HighFiveMinigame : MiniGame
{
    [Header("References")]
    [SerializeField] Transform armHinge1;
    [SerializeField] Transform armHinge2;
    [SerializeField] Transform playerArm1;
    [SerializeField] Transform playerArm2;
    [SerializeField] Animator animatorController;

    [Header("Values")]
    [SerializeField] float highFiveSpeed;
    [SerializeField] float rotateSpeed = 2f;
    [SerializeField] float raiseSpeed = 2f;
    [SerializeField] float minAngle1 = -140;
    [SerializeField] float minAngle2 = -25;
    [SerializeField] float maxAngle1 = 25;
    [SerializeField] float maxAngle2 = 140;
    [SerializeField] float minHeight = 0;
    [SerializeField] float maxHeight = 0;
    [SerializeField] float timeLimit = 7;
    [SerializeField] float heightAccuracyThreshold = 1f;
    [SerializeField] float angleAccuracyThreshold = 30f;
    [SerializeField] GameObject perfectHighFive;
    [SerializeField] GameObject okayHighFive;
    bool isHeightAccurate = false;
    bool isAngleAccurate = false;
    Timer timerScript;
    bool hasHighFived = false;

    public UnityEvent OnHighFiveSmash;
    
    public UnityEvent OnHighlowSmash;
    
    public override void StartMinigame()
    {
        timerScript = new Timer(timeLimit, Timer.TimerReset.Manual);
        timerScript.OnTimerDone += HighFive;
        StartHandSetUp(playerArm1, armHinge1, minAngle1, maxAngle1);
        StartHandSetUp(playerArm2, armHinge2, minAngle2, maxAngle2);

        HasStarted = true;
    }
    public override void ResetMinigame()
    {

    }
    void StartHandSetUp(Transform givenArm, Transform givenHinge, float givenMinAngle, float givenMaxAngle)
    {
        givenArm.position = new Vector3(givenArm.position.x, Random.Range(minHeight, maxHeight), 0);
        givenHinge.rotation = Quaternion.Euler(0, 0, Random.Range(givenMinAngle, givenMaxAngle));
    }
    void Update()
    {
        if (!HasStarted) return;

        if (!hasHighFived)
        {
            RotateHand(armHinge1, "HorizontalPlayer1", minAngle1, maxAngle1);
            RotateHand(armHinge2, "HorizontalPlayer2", minAngle2, maxAngle2);
            RaiseLowerHand(playerArm1, "VerticalPlayer1");
            RaiseLowerHand(playerArm2, "VerticalPlayer2");
        }
        timerScript.CountTimer();
    }
    void RotateHand(Transform givenHinge, string playerAxis, float minAngleAllowed, float maxAngleAllowed)
    {
        float rotateAmount = rotateSpeed * Input.GetAxis(playerAxis) * Time.deltaTime;
        givenHinge.Rotate(0, 0, rotateAmount);

        float zRotation = givenHinge.localEulerAngles.z;
        if (zRotation > 180) zRotation -= 360;

        zRotation = Mathf.Clamp(zRotation, minAngleAllowed, maxAngleAllowed);

        givenHinge.localEulerAngles = new Vector3(givenHinge.localEulerAngles.x, givenHinge.localEulerAngles.y, zRotation);
    }
    void RaiseLowerHand(Transform givenArm, string playerAxis)
    {
        float raiseAmount = raiseSpeed * Input.GetAxis(playerAxis) * Time.deltaTime;
        givenArm.Translate(0, raiseAmount, 0);
        if (givenArm.localPosition.y > maxHeight)
        {
            givenArm.position = new Vector3(givenArm.position.x, maxHeight, givenArm.position.z);
        }
        else if (givenArm.localPosition.y < minHeight)
        {
            givenArm.position = new Vector3(givenArm.position.x, minHeight, givenArm.position.z);
        }
    }
    void HighFive()
    {
        if (!hasHighFived)
        {
            Debug.Log("HighFive brother man!!");
            animatorController.SetTrigger("highFiveTime");
            hasHighFived = true;
            CalculateAccuracyScore();
        }
    }
    void CalculateAccuracyScore()
    {
        float heightDiff = Mathf.Abs(playerArm1.position.y - playerArm2.position.y);
        if (heightDiff < heightAccuracyThreshold) isHeightAccurate = true;

        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(armHinge1.rotation.eulerAngles.z, armHinge2.rotation.eulerAngles.z));
        if (angleDiff < angleAccuracyThreshold)
        {
            isAngleAccurate = true;
        }

        if (isHeightAccurate && isAngleAccurate)
        {
            //METE AQUI O SOOOOMMMMMMM
            Instantiate(perfectHighFive, (playerArm1.position + playerArm2.position) / 2, Quaternion.identity);
            ScreenShakeManager.Instance.Shake(.5f, .5f);
            Debug.Log("Perfect HighFive!");
            OnHighFiveSmash.Invoke();
        }
        else if (isHeightAccurate ^ isAngleAccurate)
        {
            //AQUI TBBBBB DANIEEEL
            Instantiate(okayHighFive, (playerArm1.position + playerArm2.position) / 2, Quaternion.identity);
            ScreenShakeManager.Instance.Shake(0.2f, 0.5f);
            Debug.Log("That was alright!");
            OnHighlowSmash.Invoke();
        }
        else
        {
            Debug.Log("Yeah, not great...");
            ScreenShakeManager.Instance.Shake(0.1f, 0.2f);
        }
        MinigameEnd();
    }
}
