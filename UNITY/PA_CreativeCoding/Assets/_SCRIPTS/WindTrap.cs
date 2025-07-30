using UnityEngine;

public class WindTrap : MonoBehaviour
{
    //Generates ForceField within Trigger to push player in Direction between two points (Written before figured out AddRelativeForce :| )

    public GameObject WindStart;
    public GameObject WindEnd;

    public float WindStrength;


    private GameObject Player;
    private Rigidbody PlayerRB;

    private Vector3 WindDirection;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerRB = Player.GetComponent<Rigidbody>();

        WindDirection = WindEnd.transform.position - WindStart.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRB.AddForce(WindDirection.normalized * WindStrength, ForceMode.Force);
        }
    }

}
