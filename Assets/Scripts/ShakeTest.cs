using UnityEngine;

public class ShakeTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScreenShakeManager.Instance.Shake(3, 0.5f);
    }
}
