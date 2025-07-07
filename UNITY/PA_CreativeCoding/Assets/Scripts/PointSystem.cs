using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private PlayerController PlayerController;
    
    private int PlayerPoints;

    public int DefaultPoints = 100;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPoints = 0;

        PlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("FlowerTier1"))
        {
            PlayerPoints += DefaultPoints;
            Debug.Log(PlayerPoints);
        }

        if(other.CompareTag("Beehive") && PlayerPoints >= 1000)
        {
            PlayerController.UnlockSpeedBoost();
        }
    }
}
