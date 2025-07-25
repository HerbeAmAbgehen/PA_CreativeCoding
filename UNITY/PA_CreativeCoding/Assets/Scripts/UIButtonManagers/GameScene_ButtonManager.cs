using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameScene_ButtonManager : MonoBehaviour
{
    public Button Resume;
    public Button Tutorial;
    public Button Quit;
    public Button Continue;
    public Button Options;
    public Button Return;

    public GameObject PauseMenu;
    public GameObject TutorialMenu;
    public GameObject Blackimage;

    public float fadeDuration;

    private bool TogglePM = false;
    private bool FirstTimeTutorial = true;

    private PlayerController PC;
    private CanvasGroup CG;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        CG = Blackimage.GetComponent<CanvasGroup>();

        StartCoroutine(FadeOutImage());
        
        Resume.onClick.AddListener(() => TogglePauseMenu());
        Continue.onClick.AddListener(() => ToggleTutorialButton());
        Tutorial.onClick.AddListener(() => ShowTutorialMenu());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        TogglePM = !TogglePM;
        if (TogglePM)
        {
            PC.TogglePause();
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
            PC.TogglePause();
        }
    }

    private void ShowTutorialMenu()
    {
        PauseMenu.SetActive(false);
        TutorialMenu.SetActive(true);
    }

    private void ToggleTutorialButton()
    {
        if (FirstTimeTutorial)
        {
            FirstTimeTutorial = false;
            TutorialMenu.SetActive(false);
            PC.TogglePause();
        }
        else
        {
            TutorialMenu.SetActive(false);
            PauseMenu.SetActive(true);
        }
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
        PC.TogglePause();
        TutorialMenu.SetActive(true);

    }
}
