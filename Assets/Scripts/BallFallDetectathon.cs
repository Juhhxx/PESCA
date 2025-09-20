using UnityEngine;

public class BallFallDetectathon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Uh oh!");
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            gameObject.transform.rotation = Quaternion.identity;
        }
}
