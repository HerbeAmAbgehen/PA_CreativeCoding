using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    //Speed at which camera rotates
    public float CameraSpeed;

    //Referenced Object to copy position from
    private GameObject Player;

    //
    private float RotationZ;

    private float RotationX;

    private float RotationY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sets referenced Object
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the objects position to that of the player object
        transform.position = Player.transform.position;

        //Gets Rotation from Horizontal / Vertical Mouse Input and AD / LR Arrow keys
        RotationZ = Input.GetAxis("Mouse Y") * CameraSpeed * Time.deltaTime;
        RotationX = Input.GetAxis("Mouse X") * CameraSpeed * Time.deltaTime;
        RotationY = Input.GetAxis("Horizontal") * -(CameraSpeed/2) * Time.deltaTime;
        
        //Rotates Object around local axis
        transform.Rotate(RotationX, RotationY, RotationZ, Space.Self);
    }
}
