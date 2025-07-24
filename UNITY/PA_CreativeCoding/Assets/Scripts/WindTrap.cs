using UnityEngine;

public class WindTrap : MonoBehaviour
{
    public GameObject WindStart;

    public GameObject WindEnd;

    private Rigidbody PlayerRB;

    private GameObject Player;

    public float WindStrength;

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
