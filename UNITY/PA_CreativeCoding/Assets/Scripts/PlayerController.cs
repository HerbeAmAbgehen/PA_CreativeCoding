using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Speed at which player moves
    public float MovementSpeed = 10;

    //ReferencedObject to copy rotation from
    private GameObject CameraTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // Sets CameraTarget as referenced Object
        CameraTarget =  GameObject.Find("CameraTarget");
    }

    // Update is called once per frame
    void Update()
    {
        //Copies Rotation From CameraTarget Rotation
        transform.rotation = CameraTarget.transform.rotation;
        
        //Moves Player forward or backward
        transform.Translate(Vector3.up.normalized * MovementSpeed * Time.deltaTime * Input.GetAxis("Vertical"));

        //Moves player up when pressing space
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.left.normalized * -(MovementSpeed / 2) * Time.deltaTime);
        }
        //Moves player down when pressing Left control
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.left.normalized * (MovementSpeed / 2) * Time.deltaTime);
        }
    }
}
