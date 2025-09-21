using UnityEngine;
using NaughtyAttributes;
public class Cloud : MonoBehaviour
{
     [MinMaxSlider(1f, 8f) ]
    [SerializeField] private Vector2 speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Random.Range(speed.x,speed.y) * Time.deltaTime;
    }
}
