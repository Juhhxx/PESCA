using UnityEngine;
using NaughtyAttributes;
public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] cloudList;
    [SerializeField] Transform[] spawnList;

    private int chosenCloud;
    private Timer timer;
    [MinMaxSlider(5f, 60f) ]
    [SerializeField] private Vector2 timerRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateTimer();
    }
    void CreateTimer()
    {
        timer = new Timer(Random.Range(timerRange.x, timerRange.y), Timer.TimerReset.Manual);
        timer.OnTimerDone += SpawnCloud;
        timer.OnTimerDone += CreateTimer;

    }

    // Update is called once per frame
    void Update()
    {
        timer.CountTimer();
    }

    private void SpawnCloud()
    {
        Instantiate(cloudList[Random.Range(0, cloudList.Length)],  spawnList[Random.Range(0,spawnList.Length)].position, Quaternion.identity);
    }
}
