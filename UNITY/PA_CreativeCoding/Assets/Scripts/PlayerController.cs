using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Speed at which player moves
    public float MovementSpeed = 10;

    //Controls maximum amount of stamina the player has
    public float MaxStamina = 100;

    //Controls how much Stamina is drained while moving
    public float StaminaDrainRate;

    //Current amount of Stamina
    private float Stamina;

    //Checks if player is game over
    private bool IsGameOver = false;

    private bool isPaused = false;

    //ReferencedObject to copy rotation from
    private GameObject CameraTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsGameOver = false;
        
        // Sets CameraTarget as referenced Object
        CameraTarget =  GameObject.Find("CameraTarget");
        //Sets Stamina to maximum on Start
        Stamina = MaxStamina;
    }




    // Update is called once per frame
    void Update()
    {
        

        //Locks player movement, if they go Game Over
        if (!IsGameOver)
        {
            GeneralMovement();
            
        }

        //If player is moving, drains Stamina
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl))
        {
            DrainStamina();
        }

        Debug.Log(Stamina);

        //Sets player Game Over, when they run out of stamina
        if(Stamina <= 0)
        {
            IsGameOver = true;
            TogglePause();
        }
    }

    private void LateUpdate()
    {
        //Locks camera movement, if player goes Game Over
        if (!IsGameOver)
        {
            CameraMovement();

        }
        
    }

    private void GeneralMovement()
    {
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

    private void CameraMovement()
    {
        //Copies Rotation From CameraTarget Rotation, when LAlt is not pressed and player is not Game Over
        if (!Input.GetKey(KeyCode.LeftAlt) && !IsGameOver)
        {
            transform.rotation = CameraTarget.transform.rotation;
        }
    }

    private void TogglePause()
    {
        //Inverts value of bool
        isPaused = !isPaused;

        //If either case is true, the game is paused
        if (isPaused || IsGameOver)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    
    private void DrainStamina()
    {
        //Decreases Stamina by fixed rate
        Stamina -= StaminaDrainRate * Time.deltaTime;
    }
}
