using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class GameScene_ButtonManager : MonoBehaviour
{
    public Button Resume;
    public Button Tutorial;
    public Button Quit;
    public Button Continue;
    public Button Options;
    public Button Return;
    public Button GORestart;
    public Button GOMenu;
    public Button Victory;
    public Button Buff;

    public GameObject PauseMenu;
    public GameObject TutorialMenu;
    public GameObject OptionsMenu;
    public GameObject Blackimage;
    public GameObject BuffPopUp;

    public float fadeDuration;

    private float DefaultCameraSpeed;
    private float DefaultMS;

    private bool TogglePM = false;
    private bool FirstTimeTutorial = true;
    private bool showOptions = false;

    private PlayerController PC;
    private CanvasGroup CG;
    private CameraTargetController CTC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        CG = Blackimage.GetComponent<CanvasGroup>();
        CTC = GameObject.Find("CameraTarget").GetComponent<CameraTargetController>();

        DefaultCameraSpeed = CTC.CameraSpeed;

        CTC.CameraSpeed = 0;


        StartCoroutine(FadeOutImage());
        
        

        Resume.onClick.AddListener(() => TogglePauseMenu());
        Continue.onClick.AddListener(() => ToggleTutorialButton());
        Tutorial.onClick.AddListener(() => ShowTutorialMenu());
        Quit.onClick.AddListener(() => LoadMenu());
        Options.onClick.AddListener(() => OptionsMenu.SetActive(true));
        Return.onClick.AddListener(() => OptionsMenu.SetActive(false));
        GORestart.onClick.AddListener(() => RestartGame());
        GOMenu.onClick.AddListener(() => LoadMenu());
        Victory.onClick.AddListener(() => LoadEndScene());
        Buff.onClick.AddListener(() => CloseBuffPopUp());
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
            PC.UnlockMovement();
            CTC.CameraSpeed = DefaultCameraSpeed;
            PC.TogglePause();
        }
        else
        {
            TutorialMenu.SetActive(false);
            PauseMenu.SetActive(true);
        }
    }

    private void CloseBuffPopUp()
    {
        BuffPopUp.SetActive(false);
        PC.UnlockSpeedBoost();
        PC.TogglePause();
        
    }

    private void RestartGame()
    {
        PC.ResetGameOver();
        SceneManager.LoadScene("GameScene");
    }

    private void LoadEndScene()
    {
        Victory.interactable = false;
        CTC.CameraSpeed = 0;
        PC.TogglePause();
        StartCoroutine(FadeInImage());
    }

    private void LoadMenu()
    {
        
        PC.ResetGameOver();
        PC.TogglePause();
        SceneManager.LoadScene("MainMenu");
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
        SceneManager.LoadScene("End");

    }
}
