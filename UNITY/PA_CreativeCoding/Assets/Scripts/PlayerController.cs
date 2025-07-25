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

    public float MaxBoost = 10f;
    //Boost 
    public float BoostDuration = 10;

    public float BoostDrain = 1;

    //Controls maximum amount of stamina the player has
    public float MaxStamina = 100;

    //Controls how much Stamina is drained while moving
    public float StaminaDrainRate;

    //Current amount of Stamina
    public float Stamina;

    //Current HP
    public int health;

    //Controls how much HP the player has at default
    public int maxHealth = 3;

    public int TimeLimit = 600;

    public int PlayTimeLeft;

    public bool BoostUnlocked = false;

    private float DefaultSpeed;

    private float MaxSpeed;

    //Checks if player is game over
    private bool IsGameOver = false;

    //Checks if game is paused
    private bool isPaused = false;

    private bool isWindAffected;

    private bool isStunned = false;

    private bool movementLocked = true;

    //ReferencedObject to copy rotation from
    private GameObject CameraTarget;

    private PointSystem PointSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayTimeLeft = TimeLimit;
        IsGameOver = false;
        movementLocked = true;
        /*isPaused = true;
        TogglePause();*/

        // Sets CameraTarget as referenced Object
        CameraTarget = GameObject.Find("CameraTarget");
        PointSystem = GetComponent<PointSystem>();
        //Sets Stamina to maximum on Start
        Stamina = MaxStamina;
        //Sets HP to default maximum at Start
        health = maxHealth;
        DefaultSpeed = MovementSpeed;
        MaxSpeed = DefaultSpeed * BoostStrength;
        BoostUnlocked = false;

       

        InvokeRepeating("TimeLeft", 3, 1);
    }




    // Update is called once per frame
    void Update()
    {


        //Locks player movement, if they go Game Over
        if (!IsGameOver && !movementLocked)
        {
            GeneralMovement();

        }

        //If player is moving, drains Stamina
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl))
        {
            DrainStamina();

        }

        //Sets player Game Over, when they run out of stamina
        if (Stamina <= 0 && !IsGameOver)
        {
            IsGameOver = true;
            GameOver();
        }

        //Game Over if Time runs out
        if (PlayTimeLeft <= 0 && !IsGameOver)
        {
            IsGameOver = true;
            GameOver();
        }


        //Speed boost
        if (Input.GetKey(KeyCode.LeftShift) && BoostDuration > 0 && BoostUnlocked)
        {
            SpeedBoost();
        }
        else if (!isStunned)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beehive"))
        {
            RefillStaminaAndBoost();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WindTrap"))
        {
            isWindAffected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WindTrap"))
        {
            isWindAffected = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isWindAffected && !isStunned)
        {
            StartCoroutine(StunTimer());
            Debug.Log("Stunned");
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
            Debug.Log(MovementSpeed);
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

    private void DrainStamina()
    {
        //Decreases Stamina by fixed rate
        Stamina -= StaminaDrainRate * Time.deltaTime;
        //Debug.Log(Stamina);
    }

    private void SpeedBoost()
    {
        MovementSpeed = MaxSpeed;
        BoostDuration -= BoostDrain * Time.deltaTime;
        //Debug.Log(BoostDuration);

    }
    private void RefillStaminaAndBoost()
    {
        Stamina = MaxStamina;
        BoostDuration = MaxBoost;
    }
    private int TimeLeft()
    {
        PlayTimeLeft = TimeLimit--;
        return PlayTimeLeft;
    }
    public void TogglePause()
    {
        //Inverts value of bool
        isPaused = !isPaused;

        //If either case is true, the game is paused
        if (isPaused ||IsGameOver)
        {
            Time.timeScale = 0f;
            Debug.Log("Pause: " + isPaused + "|GameOver: " + IsGameOver);
            Debug.Log("PAUSED");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("Pause: " + isPaused + "|GameOver: " + IsGameOver);
            Debug.Log("UNPAUSED");
        }
    }

    public void DecreaseHP()
    {
        PointSystem PS = GetComponent<PointSystem>();
        health--;
        Debug.Log(health);
        if(PS.PointsCarrying >= 10)
        {
            PS.PointsCarrying -= 10;
        }
        else if(PS.PointsCarrying < 10)
        {
            PS.PointsCarrying = 0;
        }
    }


    public void UnlockSpeedBoost()
    {
        BoostUnlocked = true;
    }


    public void GameOver()
    {
        UIManager UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        IsGameOver = true;
        TogglePause();
        UIM.GameOverPopup.SetActive(true);
    }

    public void ResetGameOver()
    {
        IsGameOver = false;
        isPaused = true;
        TogglePause();
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }

    private IEnumerator StunTimer()
    {
        isStunned = true;
        MovementSpeed *= 0.4f;
        Debug.Log(MovementSpeed);
        yield return new WaitForSeconds(5f);
        isStunned = false;
    }

}

