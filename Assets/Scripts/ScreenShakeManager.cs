using UnityEngine;
using System.Collections;

public class ScreenShakeManager : MonoBehaviourDDOL<ScreenShakeManager>
{

    private Transform camTransform;
    private Vector3 initialPos;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        base.SingletonCheck(this);
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        camTransform = Camera.main.transform;
        initialPos = Vector3.zero;
        initialPos.z = -10f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camTransform.localPosition = initialPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camTransform.localPosition = initialPos;
    }
}

