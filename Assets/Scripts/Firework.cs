using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using NaughtyAttributes;

public class Firework : MonoBehaviour
{
    private int timeToExplode;
    private Timer timer;
    Timer timerDestroy;
    [SerializeField] private ParticleSystem [] fireworks;
    [SerializeField] private ParticleSystem shockWave;
    [SerializeField] private ParticleSystem trailSparks;
    private Vector3 fireworkPos;
    private TrailRenderer trail;
    [SerializeField] private GameObject missileHeadObject;
    [SerializeField] private ParticleSystem missileHead;

    [SerializeField] private GameObject trailObject;
    [ColorUsage(true, true)]
    private Gradient chosenGradient;

    [SerializeField] private List<Gradient> gradientList;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    [MinMaxSlider(0f, 30f) ]
    [SerializeField] private Vector2 timerRange;
    [SerializeField] float secondsToDestroy;
    [SerializeField] Rigidbody2D fireworkRigidbody;
    float fireworkRiseSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    void Start()
    {
        //Set up timer da juh
        timer = new Timer(Random.Range(timerRange.x, timerRange.y), Timer.TimerReset.Manual);
        timer.OnTimerDone += Burst;
        //Get Particle System Component
        fireworkPos = GetComponent<Transform>().position;


        var main1 = fireworks[0].main;
        var main2 = fireworks[1].main;
        var sparksMain = trailSparks.main;
        var shockWaveMain = shockWave.main;
        var missileMain = missileHead.main;


        //Get Trail Renderer component
        trail = GetComponentInChildren<TrailRenderer>();

        //Based on the List of available color's length, get a random color
        int i = Random.Range(0, gradientList.Count);
        chosenGradient = gradientList[i];

        //Set the particle system's stgradient to the selected color
        main1.startColor = new ParticleSystem.MinMaxGradient(chosenGradient);

        i = Random.Range(0, gradientList.Count);
        chosenGradient = gradientList[i];
        main2.startColor = new ParticleSystem.MinMaxGradient(chosenGradient);

        sparksMain.startColor = new ParticleSystem.MinMaxGradient(chosenGradient);
        shockWaveMain.startColor = new ParticleSystem.MinMaxGradient(chosenGradient);
        missileMain.startColor = new ParticleSystem.MinMaxGradient(chosenGradient);


        if (trail != null)
        {
            trail.colorGradient = chosenGradient;
        }

        fireworkRiseSpeed = Random.Range(7.5f, 8.6f);
        fireworkRigidbody.linearVelocityY += fireworkRiseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timer.CountTimer();
        timerDestroy?.CountTimer();
    }

    private void Burst()
    {
        timerDestroy = new Timer(secondsToDestroy, Timer.TimerReset.Manual);
        timerDestroy.OnTimerDone += () => Destroy(gameObject);
        fireworkRigidbody.linearVelocityY = 0;
        trailObject.SetActive(false);
        missileHeadObject.SetActive(false);
        fireworks[0].Play();
        shockWave.Play();
        ScreenShakeManager.Instance.Shake(shakeDuration, shakeMagnitude);
        Debug.Log("Rebentei");
    }
}
