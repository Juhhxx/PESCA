using System.Collections;
using TMPro;
using UnityEngine;

public class TMPPopWave : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text tmpText;

    [Header("Pop Settings")]
    [SerializeField] private float popScale = 1.5f;       // How large each letter pops
    [SerializeField] private float popDuration = 0.2f;    // How long each pop lasts
    [SerializeField] private float delayBetweenChars = 0.05f; // Delay between each character pop

    private bool isPopping = false;

    /// <summary>
    /// Call this from another script to start the pop wave effect.
    /// </summary>
    public void PlayPop()
    {
        if (!isPopping)
        {
            StartCoroutine(PopWaveCoroutine());
        }
    }

    public void PlayWave()
    {
        if (!isPopping)
        {
            StartCoroutine(WaveCoroutine());
        }
    }

private IEnumerator WaveCoroutine()
    {
        isPopping = true;
        tmpText.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
            {
                // Start pop for this character
                StartCoroutine(PopCharacter(i));
                yield return new WaitForSeconds(delayBetweenChars);
            }
        }

        // Wait until the last pop finishes
        yield return new WaitForSeconds(popDuration);
    }

    private IEnumerator PopWaveCoroutine()
    {
        isPopping = true;
        tmpText.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
            {
                // Start pop for this character
                StartCoroutine(PopCharacter(i));
                yield return new WaitForSeconds(delayBetweenChars);
            }
        }

        // Wait until the last pop finishes
        yield return new WaitForSeconds(popDuration);
        isPopping = false;
    }

    private IEnumerator PopCharacter(int charIndex)
    {
        tmpText.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpText.textInfo;

        float timer = 0f;
        while (timer < popDuration)
        {
            float t = timer / popDuration;
            float scale = Mathf.Lerp(popScale, 1f, t);

            var charInfo = textInfo.characterInfo[charIndex];
            if (charInfo.isVisible)
            {
                int vertexIndex = charInfo.vertexIndex;
                int materialIndex = charInfo.materialReferenceIndex;
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                Vector3 center = (vertices[vertexIndex] + vertices[vertexIndex + 2]) / 2f;
                Matrix4x4 matrix = Matrix4x4.TRS(center, Quaternion.identity, Vector3.one * scale);

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] = matrix.MultiplyPoint3x4(vertices[vertexIndex + j] - center);
                }

                // Apply mesh changes
                var meshInfo = textInfo.meshInfo[materialIndex];
                meshInfo.mesh.vertices = meshInfo.vertices;
                tmpText.UpdateGeometry(meshInfo.mesh, materialIndex);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Reset vertices to default
        tmpText.ForceMeshUpdate();
    }
}
