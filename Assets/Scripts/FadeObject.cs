using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FadeObject : MonoBehaviour
{
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    public void ResetFade()
    {
        float maxAlpha = 1.0f;
        SetAlpha(maxAlpha);
    }

    public void FadeOut(float fadeDuration)
    {
        float fade = 0f;

        StartCoroutine(FadeTo(fade, fadeDuration));
    }

    private IEnumerator FadeTo(float targetAlpha, float fadeDuration)
    {
        float startAlpha = _material.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color newColor = _material.color;
        newColor.a = alpha;
        _material.color = newColor;
    }
}
