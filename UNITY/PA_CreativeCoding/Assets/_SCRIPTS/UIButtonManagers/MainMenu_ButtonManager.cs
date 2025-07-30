using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu_ButtonManager : MonoBehaviour
{
    //Controls what each Button on UI does

    public Button Play;
    public Button Options;
    public Button Quit;

    public Toggle invertMouse;
    public Slider volumeSlider;

    public AudioSource MenuClick;

    public float fadeDuration = 1f;

    private Canvas OptionsMenu;
    private Button Return;

    private CanvasGroup CG;

    private void Start()
    {
        OptionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
        Return = GameObject.Find("ButtonReturn").GetComponent<Button>();
        CG = GameObject.Find("BlackImage").GetComponent<CanvasGroup>();

        Play.onClick.AddListener(() => StartGame());
        Options.onClick.AddListener(() => ShowOptions());
        Quit.onClick.AddListener(() => ExitGame());
        Return.onClick.AddListener(() => HideOptions());


        Time.timeScale = 1f;
        StartCoroutine(FadeOutImage());
    }

    private void StartGame()
    {
        MenuClick.Play();
        StartCoroutine(FadeInImage());
    }

    private void ShowOptions()
    {
        MenuClick.Play();
        OptionsMenu.enabled = true;
    }

    private void HideOptions()
    {
        MenuClick.Play();
        OptionsMenu.enabled = false;
    }

    private void ExitGame()
    {
        MenuClick.Play();
        Application.Quit();
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
        SceneManager.LoadScene("Intro");

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
