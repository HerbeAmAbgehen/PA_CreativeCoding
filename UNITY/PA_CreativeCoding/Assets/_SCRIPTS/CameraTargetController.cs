using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    //Script controls rotation of Camera Parent Object and copies location of Player Object

    public float CameraSpeed;
    public float BounceIntensity;
    public float BounceSpeed;


    private float RotationZ;
    private float RotationX;
    private float RotationY;

    private GameObject Player;
    private GameSettings GameSettings;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");

        GameSettings = GameObject.Find("OptionsMenu").GetComponent<GameSettings>();      
    }


    private void Update()
    {
        //Gets Rotation from Horizontal / Vertical Mouse Input and AD / LR Arrow keys
        if(GameSettings.invertFlight)
        {
            RotationZ = Input.GetAxis("Mouse Y") * CameraSpeed * Time.deltaTime;
        }
        else
        {
            RotationZ = Input.GetAxis("Mouse Y") * -CameraSpeed * Time.deltaTime;
        }
        RotationX = Input.GetAxis("Mouse X") * CameraSpeed * Time.deltaTime;
        RotationY = Input.GetAxis("Horizontal") * -(CameraSpeed / 2) * Time.deltaTime;

        //Rotates Object around local axis
        transform.Rotate(RotationX, RotationY, RotationZ, Space.Self);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Sets the objects position to that of the player object
        transform.position = Player.transform.position;     

        //Uses Sinean Function to generate Offset for Bounce Method
        float Offset = Mathf.Sin(BounceSpeed * Time.time) * BounceIntensity;
        CameraBounce(Offset);
    }

    //Moves Camera Up and Down on Y Axis
    private void CameraBounce(float BounceOffset)
    {
        //Moves Object along Y Axis depending on given offset variable
        transform.position = new Vector3(transform.position.x, transform.position.y + BounceOffset, transform.position.z);
    }
}
