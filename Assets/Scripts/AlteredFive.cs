using UnityEngine;
using UnityEngine.Events;

public class AlteredFive : MiniGame
{
    [Header("References")]
    [SerializeField] Transform armHinge1;
    [SerializeField] Transform armHinge2;
    [SerializeField] Transform playerArm1;
    [SerializeField] Transform playerArm2;
    [SerializeField] Animator animatorController;

    [Header("Values")]
    [SerializeField] float highFiveSpeed;
    [SerializeField] float rotateSpeed1 = 2f;
    [SerializeField] float rotateSpeed2 = 2f;
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
    public UnityEvent Whoosh;

    private void Start()
    {
       
    }

    public override void StartMinigame()
    {
        

        timerScript = new Timer(timeLimit, Timer.TimerReset.Manual);
        timerScript.OnTimerDone += HighFive;
        

        HasStarted = true;
    }
    public override void ResetMinigame()
    {

    }
    
    void Update()
    {
        if (!HasStarted) return;

        if (!hasHighFived)
        {
            RotateHand(armHinge1, "HorizontalPlayer1", minAngle1, maxAngle1, rotateSpeed1);
            RotateHand(armHinge2, "HorizontalPlayer2", minAngle2, maxAngle2, rotateSpeed2);
            RaiseLowerHand(playerArm1, "VerticalPlayer1");
            RaiseLowerHand(playerArm2, "VerticalPlayer2");
        }
        timerScript.CountTimer();
    }
    void RotateHand(Transform givenHinge, string playerAxis, float givenMinAngleAllowed, float givenMaxAngleAllowed, float givenRotateSpeed)
    {
        float rotateAmount = givenRotateSpeed * Input.GetAxis(playerAxis) * Time.deltaTime;
        givenHinge.Rotate(0, 0, rotateAmount);

        float zRotation = givenHinge.localEulerAngles.z;
        if (zRotation > 180) zRotation -= 360;

        zRotation = Mathf.Clamp(zRotation, givenMinAngleAllowed, givenMaxAngleAllowed);

        givenHinge.localEulerAngles = new Vector3(givenHinge.localEulerAngles.x, givenHinge.localEulerAngles.y, zRotation);
    }
    void RaiseLowerHand(Transform givenArm, string playerAxis)
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

            MinigameEnd();
        }
    }
    
}
