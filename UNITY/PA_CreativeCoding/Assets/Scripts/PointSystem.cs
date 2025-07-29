using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private PlayerController PlayerController;

    public int PointsCarrying;

    public int PointsHive;

    public int DefaultPoints = 100;

    public UIManager UIManager;

    public AudioSource PlayerAudio;

    public AudioSource GlobalAudio;

    public AudioClip PointsPickUp;

    public AudioClip PointsDeliver;

    public AudioClip Victory;

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
            GlobalAudio.clip = PointsPickUp;
            GlobalAudio.Play();
            PointsCarrying += DefaultPoints;
            Debug.Log(PointsCarrying);
            other.gameObject.GetComponent<MeshRenderer>().materials[2].color = Color.white;
            other.enabled = false;
        }

        if (other.CompareTag("Beehive"))
        {
            GlobalAudio.clip = PointsDeliver;
            GlobalAudio.Play();
            PointsHive += PointsCarrying;
            PointsCarrying = 0;
            if (!PlayerController.BoostUnlocked && PointsHive >= 55)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PlayerAudio.Stop();
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerAudio.Stop();
            GlobalAudio.clip = Victory;
            GlobalAudio.Play();
            hasWon = true;
            PlayerController.TogglePause();
            UIManager.VictoryPopUp.SetActive(true);
        }
    }
}
