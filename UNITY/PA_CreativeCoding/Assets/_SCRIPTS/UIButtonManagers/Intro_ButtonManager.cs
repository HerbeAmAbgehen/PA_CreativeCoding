using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class Intro_ButtonManager : MonoBehaviour
{
    //Controls what each Button on UI does

    public Button Continue;
    public GameObject BlackImage;
    public AudioSource MenuClick;

    public float fadeDuration;

    private CanvasGroup CG;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CG = BlackImage.GetComponent<CanvasGroup>();

        StartCoroutine(FadeOutImage());

        Continue.onClick.AddListener(() => FadeNextScene());
    }

    private void FadeNextScene()
    {
        MenuClick.Play();
        StartCoroutine(FadeInImage());
    }

    //Fades image to full transparency
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

    //Fades Image to full opacity before changing scene
    private IEnumerator FadeInImage()
    {

        float t = 0f;
        while (t < fadeDuration)

        {

            t += Time.deltaTime;
            CG.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;

        }

        CG.alpha = 1f;
        SceneManager.LoadScene("GameScene");

    }
}
