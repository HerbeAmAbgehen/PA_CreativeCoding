using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu_ButtonManager : MonoBehaviour
{

    public Button Play;
    public Button Options;
    public Button Quit;

    public Toggle invertMouse;
    public Slider volumeSlider;


    private Canvas OptionsMenu;
    private Button Return;

    private void Start()
    {
        OptionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
        Return = GameObject.Find("ButtonReturn").GetComponent<Button>();

        Play.onClick.AddListener(() => SceneManager.LoadScene("Intro"));
        Options.onClick.AddListener(() => OptionsMenu.enabled = true);
        Quit.onClick.AddListener(() => Application.Quit());
        Return.onClick.AddListener(() => OptionsMenu.enabled = false);


        Time.timeScale = 1f;
    }
}
