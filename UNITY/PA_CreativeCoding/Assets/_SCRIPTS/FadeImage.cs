using System.Collections;
using UnityEngine;

public class FadeImage : MonoBehaviour
{
    //Used to Fade a black image on scene changes to smooth transitions between scenes

    public GameObject BlackImage;

    public int fadeDuration;

    private CanvasGroup CG;

    private void Start()
    {
        CG = BlackImage.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }

    //Fades black image to full opacity and then exits application
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


    //Fades image to full transparency
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

    //Used to call Coroutine from other scripts
    public void FadeQuit()
    {
        StartCoroutine(FadeOutQuit());
    }
}
