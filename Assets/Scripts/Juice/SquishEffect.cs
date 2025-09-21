using UnityEngine;
using System.Collections;

public class SquishEffect : MonoBehaviour
{
    [SerializeField] private float squishAmount = 0.5f; // how much to squish
    [SerializeField] private float duration = 0.2f;     // how fast the squish is

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void PlaySquish()
    {
        StopAllCoroutines();
        StartCoroutine(SquishCoroutine());
    }

    private IEnumerator SquishCoroutine()
    {
        Vector3 squishedScale = new Vector3(originalScale.x + squishAmount, 
                                            originalScale.y - squishAmount, 
                                            originalScale.z);

        // Squish down
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, t);
            yield return null;
        }

        // Return to normal
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale; // reset just in case
    }
}

