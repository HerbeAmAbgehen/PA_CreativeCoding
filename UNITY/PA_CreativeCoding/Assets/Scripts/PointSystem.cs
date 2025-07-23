using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private PlayerController PlayerController;
    
    public int PointsCarrying;

    public int PointsHive;

    public int DefaultPoints = 100;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointsCarrying = 0;
        PointsHive = 0;
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
            PointsCarrying += DefaultPoints;
            Debug.Log(PointsCarrying);
        }

        if(other.CompareTag("Beehive"))
        {
            PointsHive += PointsCarrying;
            PointsCarrying = 0;
            if(!PlayerController.BoostUnlocked && PointsCarrying >= 50)
            {
                PlayerController.UnlockSpeedBoost();
            }
        }

        
    }
}
