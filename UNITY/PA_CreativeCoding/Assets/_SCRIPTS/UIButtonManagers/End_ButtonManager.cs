using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End_ButtonManager : MonoBehaviour
{
    //Controls what each Button on UI does

    public Button Menu;
    public GameObject BlackImage;
    public AudioSource GlobalAudio;

    public float fadeDuration;

    private CanvasGroup CG;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CG = BlackImage.GetComponent<CanvasGroup>();

        StartCoroutine(FadeOutImage());

        Menu.onClick.AddListener(() => LoadMenuScene());
    }

    private void LoadMenuScene()
    {
        GlobalAudio.Play();
        StartCoroutine(FadeInImage());
    }

    //Fades in image and loads main menu
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
        SceneManager.LoadScene("MainMenu");

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
}
