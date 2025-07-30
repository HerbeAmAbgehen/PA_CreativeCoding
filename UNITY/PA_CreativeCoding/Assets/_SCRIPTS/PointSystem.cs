using UnityEngine;

public class PointSystem : MonoBehaviour
{
    //Script controls points collection/delivery and victory condition

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

    private PlayerController PlayerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        PointsCarrying = 0;
        PointsHive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for victory condition
        VictoryPopUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Adds points to player "inventory" when entering flower
        if (other.CompareTag("FlowerTier1"))
        {
            GlobalAudio.clip = PointsPickUp;
            GlobalAudio.Play();
            PointsCarrying += DefaultPoints;
            Debug.Log(PointsCarrying);
            other.gameObject.GetComponent<MeshRenderer>().materials[2].color = Color.white;
            other.enabled = false;
        }

        //Adds players points to total score, unlocks speedboost when total score is 55 or more
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
                UIManager.BoostIcon.SetActive(true);
                PlayerController.TogglePause();
            }
        }
    }


    //If total score is 100 or more pauses game and enables victory UI Panel
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
