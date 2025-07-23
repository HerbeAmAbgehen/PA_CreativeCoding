using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject Player;

    public GameObject StaminaBar;

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
    }

    // Update is called once per frame
    void Update()
    {
        MatchUI();
    }

    private void MatchUI()
    {
        StaminaFill.fillAmount = PC.Stamina / PC.MaxStamina;
        ScoreCarrying.text = $"{PointSystem.PointsCarrying}";
        ScoreHive.text = $"{PointSystem.PointsHive}";

    }
}
