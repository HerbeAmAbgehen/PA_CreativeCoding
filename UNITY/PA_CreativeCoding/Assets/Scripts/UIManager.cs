using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject Player;

    public GameObject StaminaBar;

    public GameObject BuffPopup;

    public GameObject GameOverPopup;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;


    public TMP_Text ScoreCarrying;

    public TMP_Text ScoreHive;

    private PlayerController PC;

    private PointSystem PointSystem;

    private Image StaminaFill;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PC = Player.GetComponent<PlayerController>();
        PointSystem = Player.GetComponent<PointSystem>();
        StaminaFill = StaminaBar.GetComponent<Image>();
        ScoreCarrying.text = $"{00}";
        ScoreHive.text = $"{00}";
    }

    // Update is called once per frame
    void Update()
    {
        MatchUI();
        Heartbreak();
    }

    private void MatchUI()
    {
        StaminaFill.fillAmount = PC.Stamina / PC.MaxStamina;
        ScoreCarrying.text = $"{PointSystem.PointsCarrying}";
        ScoreHive.text = $"{PointSystem.PointsHive}";

    }

    private void Heartbreak()
    {
        switch (PC.health)
        {
            case 2:
                Heart3.SetActive(false);
                break;

            case 1:
                Heart2.SetActive(false);
                break;

            case 0:
                Heart1.SetActive(false);
                PC.GameOver();
                break;
        }
    }
}
