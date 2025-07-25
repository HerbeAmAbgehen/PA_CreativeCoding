using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private PlayerController PlayerController;

    public int PointsCarrying;

    public int PointsHive;

    public int DefaultPoints = 100;

    public UIManager UIManager;

    private bool hasWon = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointsCarrying = 0;
        PointsHive = 0;
        PlayerController = GetComponent<PlayerController>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        VictoryPopUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlowerTier1"))
        {
            PointsCarrying += DefaultPoints;
            Debug.Log(PointsCarrying);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Beehive"))
        {
            PointsHive += PointsCarrying;
            PointsCarrying = 0;
            if (!PlayerController.BoostUnlocked && PointsHive >= 55)
            {
                UIManager.BuffPopup.SetActive(true);
                UIManager.Boost.SetActive(true);
                PlayerController.TogglePause();
            }
        }
    }


    private void VictoryPopUp()
    {
        if(PointsHive >= 100 && !hasWon)
        {
            hasWon = true;
            PlayerController.TogglePause();
            UIManager.VictoryPopUp.SetActive(true);
        }
    }
}
