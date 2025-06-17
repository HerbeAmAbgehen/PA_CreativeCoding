using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float movementSpeed;

    public float turnStrength;

    private Vector3 startPosition;

    private GameObject player;

    private PlayerController playerController;

    private int behaviourState;
    private bool AttackCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Saves Start Position
        startPosition = transform.position;

        player = GameObject.Find("Player");

        playerController = player.GetComponent<PlayerController>();

        //Sets Behaviour state to Patrol
        behaviourState = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Toggles which behaviour to use depending on state
        switch (behaviourState)
        {
            case 0:
                AggroBehaviour();
                break;
            case 1:
                ReturnBehaviour();
                break;
            case 2:
                PatrolBehaviour();
                break;
        }

    }

    //Default Behaviour when Enemy is not aggroed, object moves in circles
    private void PatrolBehaviour()
    {
        //Moves object forward
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        //Rotates object and causes it to move in circles
        transform.Rotate(Vector3.up, turnStrength * Time.deltaTime, Space.Self);

    }

    //When enemy is no longer in Range of Player, return to starting position, before setting behaviour to patrol
    private void ReturnBehaviour()
    {
        //Calculates Direction towards Start Position
        Vector3 ReturnDirection = startPosition - transform.position;
        
        //Moves object directly to start position
        transform.Translate(ReturnDirection.normalized * movementSpeed * Time.deltaTime);
        
        //Sets Object to Start position when close enough and resets behaviour state to patrol
        if(ReturnDirection.magnitude < 0.1f)
        {
            transform.position = startPosition;
            behaviourState = 2;
        }
    }

    //Follows player and collides, when in Range
    private void AggroBehaviour()
    {
        //Calculates Direction towards player
        Vector3 PlayerDirection = player.transform.position - transform.position;

        //Moves object towards player
        transform.Translate(PlayerDirection.normalized * movementSpeed * Time.deltaTime);
    }

    //When player enters Trigger, sets behaviour to Aggro
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            behaviourState = 0;
        }
    }

    //When player leaves Trigger, sets behaviour state to Return
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            behaviourState = 1;
        }
    }

    //Decreases player HP on collision and starts Attack Cooldown Coroutine
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !AttackCooldown)
        {
            StartCoroutine(PostAttackInvincibility());
            playerController.DecreaseHP();
        }
    }

    //Prevents Enemies from damaging player for 1 second
    private IEnumerator PostAttackInvincibility()
    {
        AttackCooldown = true;
        yield return new WaitForSeconds(1);
        AttackCooldown = false;
    }
}

