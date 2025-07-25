using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class Intro_ButtonManager : MonoBehaviour
{
    public Button Continue;

    public GameObject BlackImage;

    public float fadeDuration;

    private CanvasGroup CG;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CG = BlackImage.GetComponent<CanvasGroup>();

        Continue.onClick.AddListener(() => FadeNextScene());

        StartCoroutine(FadeOutImage());
    }

    private void FadeNextScene()
    {
        StartCoroutine(FadeInImage());
    }

    private IEnumerator FadeOutImage()
    {

        float t = 0f;
        while (t < fadeDuration)

        {

            t += Time.deltaTime;
            CG.alpha = 1 - Mathf.Clamp01(t / fadeDuration);
            yield return null;

        }

        CG.alpha = 0f;

    }

    private IEnumerator FadeInImage()
    {

        float t = 0f;
        while (t < fadeDuration)

        {

            t += Time.deltaTime;
            CG.alpha = 1f - Mathf.Clamp01(t / fadeDuration);
            yield return null;

        }

        CG.alpha = 1f;
        SceneManager.LoadScene("GameScene");

    }
}
