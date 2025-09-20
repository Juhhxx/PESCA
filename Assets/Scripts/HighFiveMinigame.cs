using UnityEngine;
using UnityEngine.UIElements;

public class HighFiveMinigame : MiniGame
{
    [SerializeField] Transform armHinge1;
    [SerializeField] Transform armHinge2;
    [SerializeField] Transform playerArm1;
    [SerializeField] Transform playerArm2;
    [SerializeField] float highFiveSpeed;
    [SerializeField] float rotateSpeed = 2f;
    [SerializeField] float minAngle1 = -140;
    [SerializeField] float minAngle2 = -25;
    [SerializeField] float maxAngle1 = 25;
    [SerializeField] float maxAngle2 = 140;
    [SerializeField] float timeLimit = 7;
    Timer timerScript;
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
        RotateHand(armHinge1, "HorizontalPlayer1", minAngle1, maxAngle1);
        RotateHand(armHinge2, "HorizontalPlayer2", minAngle2, maxAngle2);
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
    void HighFive()
    {
        Debug.Log("HighFive brother man!!");
        playerArm1.Translate(highFiveSpeed, 0, 0);
        playerArm2.Translate(-highFiveSpeed, 0, 0);
    }
}
