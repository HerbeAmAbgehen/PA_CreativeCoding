using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu_ButtonManager : MonoBehaviour
{

    public Button Play;
    public Button Options;
    public Button Quit;
    public Button Return;


    public GameObject OptionsMenu;

    private void Start()
    {
        Play.onClick.AddListener(() => SceneManager.LoadScene("Intro"));
        Options.onClick.AddListener(() => OptionsMenu.SetActive(true));
        Quit.onClick.AddListener(() => Application.Quit());
        Return.onClick.AddListener(() => OptionsMenu.SetActive(false));
    }
}
