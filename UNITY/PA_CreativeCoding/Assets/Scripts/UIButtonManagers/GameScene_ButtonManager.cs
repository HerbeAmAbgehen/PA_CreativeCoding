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
    public Button GORestart;
    public Button GOMenu;
    public Button Victory;
    public Button Buff;

    public GameObject PauseMenu;
    public GameObject TutorialMenu;
    public GameObject Blackimage;
    public GameObject BuffPopUp;

    public AudioSource PlayerAudio;
    public AudioSource GlobalAudio;

    public AudioClip MenuClick;

    public float fadeDuration;

    private float DefaultCameraSpeed;
    private float DefaultMS;

    private bool TogglePM = false;
    private bool FirstTimeTutorial = true;
    private bool showOptions = false;

    private PlayerController PC;
    private CanvasGroup CG;
    private CameraTargetController CTC;
    private Canvas OM;
    private Button Return;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        CG = Blackimage.GetComponent<CanvasGroup>();
        CTC = GameObject.Find("CameraTarget").GetComponent<CameraTargetController>();
        OM = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
        Return = GameObject.Find("ButtonReturn").GetComponent<Button>();

        DefaultCameraSpeed = CTC.CameraSpeed;

        CTC.CameraSpeed = 0;


        StartCoroutine(FadeOutImage());
        
        

        Resume.onClick.AddListener(() => TogglePauseMenu());
        Continue.onClick.AddListener(() => ToggleTutorialButton());
        Tutorial.onClick.AddListener(() => ShowTutorialMenu());
        Quit.onClick.AddListener(() => LoadMenu());
        Options.onClick.AddListener(() => ShowOptions());
        Return.onClick.AddListener(() => HideOptions());
        GORestart.onClick.AddListener(() => RestartGame());
        GOMenu.onClick.AddListener(() => LoadMenu());
        Victory.onClick.AddListener(() => LoadEndScene());
        Buff.onClick.AddListener(() => CloseBuffPopUp());

        PlayerAudio.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerAudio.Stop();
            PC.TogglePause();
            PauseMenu.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerAudio.Play();
            PauseMenu.SetActive(false);
            OM.enabled = false;
            PC.TogglePause();
        }
    }

    private void ShowTutorialMenu()
    {
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        PauseMenu.SetActive(false);
        TutorialMenu.SetActive(true);
    }

    private void ToggleTutorialButton()
    {
        if (FirstTimeTutorial)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GlobalAudio.clip = MenuClick;
            GlobalAudio.Play();
            PlayerAudio.Play();
            FirstTimeTutorial = false;
            TutorialMenu.SetActive(false);
            PC.UnlockMovement();
            CTC.CameraSpeed = DefaultCameraSpeed;
            PC.TogglePause();
        }
        else
        {
            GlobalAudio.clip = MenuClick;
            GlobalAudio.Play();
            TutorialMenu.SetActive(false);
            PauseMenu.SetActive(true);
        }
    }

    private void CloseBuffPopUp()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        BuffPopUp.SetActive(false);
        PC.UnlockSpeedBoost();
        PlayerAudio.Play();
        PC.TogglePause();
        
    }

    private void RestartGame()
    {
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        PC.ResetGameOver();
        SceneManager.LoadScene("GameScene");
    }

    private void LoadEndScene()
    {
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        Victory.interactable = false;
        CTC.CameraSpeed = 0;
        PC.TogglePause();
        StartCoroutine(FadeInImage("End"));
    }

    private void LoadMenu()
    {
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        PC.ResetGameOver();
        StartCoroutine(FadeInImage("MainMenu"));
    }

    private void ShowOptions()
    {
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        OM.enabled = true;
    }

    private void HideOptions()
    {
        GlobalAudio.clip = MenuClick;
        GlobalAudio.Play();
        OM.enabled = false;
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
        PlayerAudio.Stop();
        TutorialMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator FadeInImage(string SceneName)
    {

        float t = 0f;
        while (t < fadeDuration)

        {

            t += Time.deltaTime;
            CG.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;

        }

        CG.alpha = 1f;
        SceneManager.LoadScene(SceneName);

    }
}
