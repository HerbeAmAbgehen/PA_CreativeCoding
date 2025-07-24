using System.Collections;
using UnityEngine;

public class FadeImage : MonoBehaviour
{
    public GameObject BlackImage;

    public int fadeDuration;

    private CanvasGroup CG;

    private void Start()
    {
        CG = BlackImage.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOutQuit()
    {

        float t = 0f;
        while (t < fadeDuration)

        {

            t += Time.deltaTime;
            CG.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;

        }

        CG.alpha = 1f;
        Application.Quit();

    }

    private IEnumerator FadeIn()
    {

        float t = 0f;
        while (t < fadeDuration)

        {

            t += Time.deltaTime;
            CG.alpha = 1f - Mathf.Clamp01(t / fadeDuration);
            yield return null;

        }

        CG.alpha = 0f;

    }

    public void FadeQuit()
    {
        StartCoroutine(FadeOutQuit());
    }
}
