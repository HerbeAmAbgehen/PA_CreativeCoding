using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Script contains Player Controls, conditions for game over, PlayTimer, controls trigger events

    public float MovementSpeed = 10;
    public float BoostStrength = 1.5f;
    public float MaxBoost = 10f;
    public float BoostDuration = 10;
    public float BoostDrain = 1;
    public float MaxStamina = 100;
    public float StaminaDrainRate;
    public float Stamina;

    public int health;
    public int maxHealth = 3;
    public int TimeLimit = 600;
    public int PlayTimeLeft;

    public bool BoostUnlocked = false;

    public AudioSource GlobalAudio;
    public AudioClip HitRock;


    private float DefaultSpeed;
    private float MaxSpeed;
    private float DefaultPitch;
    private float HighPitch;

    private bool IsGameOver = false;
    private bool isPaused = false;
    private bool isWindAffected;
    private bool isStunned = false;
    private bool movementLocked = true;
    private bool blockGameOver = false;

    private GameObject CameraTarget;
    private Rigidbody PlayerRb;
    private AudioSource PlayerAudio;

    // Gets information on referenced objects and components, Sets fixed values to given defaults, sets important bools to correct start values
    void Start()
    {
        CameraTarget = GameObject.Find("CameraTarget");
        PlayerRb = GetComponent<Rigidbody>();
        PlayerAudio = GetComponent<AudioSource>();

        PlayTimeLeft = TimeLimit;
        Stamina = MaxStamina;
        health = maxHealth;
        DefaultSpeed = MovementSpeed;
        MaxSpeed = DefaultSpeed * BoostStrength;
        DefaultPitch = PlayerAudio.pitch;
        HighPitch = DefaultPitch * 1.1f;

        IsGameOver = false;
        movementLocked = true;
        blockGameOver = false;
        BoostUnlocked = false;

        //Calls Timer function
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
        if (Stamina <= 0 && !IsGameOver && !blockGameOver)
        {
            IsGameOver = true;
            Debug.Log("No Stamina");
            GameOver();
        }

        //Game Over if Time runs out
        if (PlayTimeLeft <= 0 && !IsGameOver && !blockGameOver)
        {
            IsGameOver = true;
            Debug.Log("No Time left");
            GameOver();
        }

        //Calls speed boost function
        if (Input.GetKey(KeyCode.LeftShift) && BoostDuration > 0 && BoostUnlocked)
        {
            SpeedBoost();
        }
        else if (!isStunned)
        {
            PlayerAudio.pitch = DefaultPitch;
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
        //Refillst Stamina and Speed Boost when player returns to hive
        if (other.CompareTag("Beehive"))
        {
            RefillStaminaAndBoost();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Checks if player is currently inside WindTrap
        if (other.CompareTag("WindTrap"))
        {
            isWindAffected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Resets bool if player exits WindTrap
        if (other.CompareTag("WindTrap"))
        {
            isWindAffected = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If player hits object with "obstacle" tag while inside WindTrap, applies slow
        if (collision.gameObject.CompareTag("Obstacle") && isWindAffected && !isStunned)
        {
            GlobalAudio.clip = HitRock;
            GlobalAudio.Play();
            StartCoroutine(StunTimer());
            Debug.Log("Stunned");
        }
    }


    //Main function to move player object around (Reworked to use AddForce to fix collider clipping)
    private void GeneralMovement()
    {
        //Moves Player forward or backward
        //transform.Translate(Vector3.up.normalized * MovementSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        PlayerRb.AddRelativeForce(Vector3.up * MovementSpeed * 5.7f *Time.deltaTime * Input.GetAxis("Vertical"), ForceMode.VelocityChange);


        //Moves player up when pressing space
        if (Input.GetKey(KeyCode.Space))
        {
            //transform.Translate(Vector3.left.normalized * -(MovementSpeed / 2) * Time.deltaTime);
            PlayerRb.AddRelativeForce(Vector3.left * -(MovementSpeed / 2) * 7 * Time.deltaTime, ForceMode.VelocityChange);
            Debug.Log(MovementSpeed);
        }
        //Moves player down when pressing Left control
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            PlayerRb.AddRelativeForce(Vector3.left * (MovementSpeed / 2) * 7 * Time.deltaTime, ForceMode.VelocityChange);
            //transform.Translate(Vector3.left.normalized * (MovementSpeed / 2) * Time.deltaTime);
        }
    }
    
    //Uses rotation of CameraTarget to control rotation
    private void CameraMovement()
    {
        //Copies Rotation From CameraTarget Rotation, when LAlt is not pressed and player is not Game Over
        if (!Input.GetKey(KeyCode.LeftAlt) && !IsGameOver)
        {
            transform.rotation = CameraTarget.transform.rotation;
        }
    }

    //Decreases Stamina by fixed rate
    private void DrainStamina()
    {
        Stamina -= StaminaDrainRate * Time.deltaTime;
    }

    //Applies speed boost when called
    private void SpeedBoost()
    {
        PlayerAudio.pitch = HighPitch;
        MovementSpeed = MaxSpeed;
        BoostDuration -= BoostDrain * Time.deltaTime;
    }

    //Called when player enters beehive, refills stamina and speedboost
    private void RefillStaminaAndBoost()
    {
        Stamina = MaxStamina;
        BoostDuration = MaxBoost;
    }

    //Decreases Time left by 1 second on call
    private int TimeLeft()
    {
        PlayTimeLeft = TimeLimit--;
        return PlayTimeLeft;
    }

    //Sets timescale to 0 or 1, pausing the game, switches on call
    public void TogglePause()
    {
        //Inverts value of bool
        isPaused = !isPaused;

        //If either case is true, the game is paused
        if (isPaused ||IsGameOver)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    //Decreases hp by 1 and points carrying by 10 on call, if player carries less than 10 points, sets points to 0
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

    //Unlocks speed boost by setting bool on call
    public void UnlockSpeedBoost()
    {
        BoostUnlocked = true;
    }

    //Sets GameOver bool to true and pauses game, shows UI Panel and unlocks cursor
    public void GameOver()
    {
        UIManager UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        IsGameOver = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        PlayerAudio.Stop();
        TogglePause();
        UIM.GameOverPopup.SetActive(true);
    }

    //Resets GameOver conditions and blocks them from applying until scene reload
    public void ResetGameOver()
    {
        IsGameOver = false;
        isPaused = true;
        blockGameOver = true;
        TogglePause();
    }

    //Used to block player movement until called
    public void UnlockMovement()
    {
        movementLocked = false;
    }

    //Applies Slow effect for five seconds, called when player gets stunned by wind
    private IEnumerator StunTimer()
    {
        isStunned = true;
        MovementSpeed *= 0.4f;
        Debug.Log(MovementSpeed);
        yield return new WaitForSeconds(5f);
        isStunned = false;
    }

}

