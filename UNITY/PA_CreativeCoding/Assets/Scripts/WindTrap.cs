using UnityEngine;

public class WindTrap : MonoBehaviour
{
    private Rigidbody PlayerRB;

    private GameObject Player;

    public float WindStrength;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerRB = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRB.AddForce(Vector3.left * WindStrength, ForceMode.Impulse);
        }
    }
}
