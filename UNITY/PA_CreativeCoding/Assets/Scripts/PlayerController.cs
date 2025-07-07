using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool InvertMouse = false;

    //Speed at which player moves
    public float MovementSpeed = 10;

    //Speed boost
    public float BoostStrength = 1.5f;

    //Boost 
    public float BoostDuration = 10;

    public float BoostDrain = 1;

    //Controls maximum amount of stamina the player has
    public float MaxStamina = 100;

    //Controls how much Stamina is drained while moving
    public float StaminaDrainRate;

    //Controls how much HP the player has at default
    public int maxHealth = 3;

    public GameObject GOText;

    private float DefaultSpeed;
    private float MaxSpeed;

    //Current amount of Stamina
    private float Stamina;

    //Current HP
    private int health;

    //Checks if player is game over
    private bool IsGameOver = false;

    //Checks if game is paused
    private bool isPaused = false;

    private bool BoostUnlocked = false;

    //ReferencedObject to copy rotation from
    private GameObject CameraTarget;

    private PointSystem PointSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsGameOver = false;

        // Sets CameraTarget as referenced Object
        CameraTarget = GameObject.Find("CameraTarget");
        PointSystem = GetComponent<PointSystem>();
        //Sets Stamina to maximum on Start
        Stamina = MaxStamina;
        //Sets HP to default maximum at Start
        health = maxHealth;
        //Disables GameOver text
        GOText.SetActive(false);
        DefaultSpeed = MovementSpeed;
        MaxSpeed = DefaultSpeed * BoostStrength;
        BoostUnlocked = false;

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

        //Sets player Game Over, when they run out of stamina
        if (Stamina <= 0)
        {
            IsGameOver = true;
            TogglePause();
        }

        //Sets player Game Over, when health is at 0
        if (health <= 0)
        {
            IsGameOver = true;
            TogglePause();
        }

        //Enables GameOver Text
        if (IsGameOver)
        {
            GOText.SetActive(true);
        }


        //Speed boost
        if (Input.GetKey(KeyCode.LeftShift) && BoostDuration > 0 && BoostUnlocked)
        {
            SpeedBoost();
        }
        else
        {
            MovementSpeed = DefaultSpeed;
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
        //Debug.Log(Stamina);
    }

    public void DecreaseHP()
    {
        health--;
        Debug.Log(health);
    }


    private void SpeedBoost()
    {
        MovementSpeed = MaxSpeed;
        BoostDuration -= BoostDrain * Time.deltaTime;
        Debug.Log(BoostDuration);
        
    }

    public void UnlockSpeedBoost()
    {
        BoostUnlocked = true;
    }
}

