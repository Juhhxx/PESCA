using NUnit.Framework;
using UnityEngine;

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
    bool isHeightAccurate = false;
    bool isAngleAccurate = false;
    Timer timerScript;
    bool hasHighFived = false;
    public override void StartMinigame()
    {

    }
    public override void ResetMinigame()
    {

    }
    void Start()
    {
        timerScript = new Timer(timeLimit, Timer.TimerReset.Manual);
        timerScript.OnTimerDone += HighFive;
    }
    void Update()
    {
        if (!hasHighFived)
        {
            RotateHand(armHinge1, "HorizontalPlayer1", minAngle1, maxAngle1);
            RotateHand(armHinge2, "HorizontalPlayer2", minAngle2, maxAngle2);
            RaiseLowerHand(playerArm1, "VerticalPlayer1", minHeight, maxHeight);
            RaiseLowerHand(playerArm2, "VerticalPlayer2", minHeight, maxHeight);
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
    void RaiseLowerHand(Transform givenArm, string playerAxis, float minHeightAllowed, float maxHeightAllowed)
    {
        float raiseAmount = raiseSpeed * Input.GetAxis(playerAxis) * Time.deltaTime;
        givenArm.Translate(0, raiseAmount, 0);
        if (givenArm.position.y > maxHeight)
        {
            givenArm.position = new Vector3(givenArm.position.x, maxHeight, givenArm.position.z);
        }
        else if (givenArm.position.y < minHeight)
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
        if (angleDiff < angleAccuracyThreshold) isAngleAccurate = true;

        if (isHeightAccurate && isAngleAccurate) Debug.Log("Perfect HighFive!");
        else if (isHeightAccurate ^ isAngleAccurate) Debug.Log("That was alright!");
        else Debug.Log("Yeah, not great...");
    }
}
