using UnityEngine;

public class SpaceEnemy : MonoBehaviour
{
    public Transform player;
    public float speed;
    float currentSpeed;

    bool charge = true; //Determine when enemy moves backward
    float timer = 0f; //Duration of moving backward

    float deceleration = 0f;


    private void Start()
    {
        currentSpeed = speed;
    }

    private void Update()
    {
        //The logic of enemy move: does enemy need to increase/decrease speed? moving backward in order to charge, then rush/sprint towards player!
        AdjustSpeed();
        enemyWait();
        enemyRush();
    }

    public void enemyWait() //Moving backwards of player, at beginning and when touches player
    {
        if (charge)
        {
            timer += Time.deltaTime;
            Vector3 deDirection = transform.position - player.transform.position; //Back direction
            transform.position += deDirection.normalized * currentSpeed / 4 * Time.deltaTime;
            if (timer > 3) //moving 3 seconds bacwards
            {
                charge = false;
                timer = 0f;
            }
        }

    }

    public void enemyRush() //chasing player
    {
        if (charge == false) //after finished moving backwards
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.position += direction.normalized * currentSpeed * Time.deltaTime;
            if (direction.magnitude < 1) //if distance between enemy & player < 1
            {
                charge = true; //Means enemy got and hurted player, moving backward again and getting ready fo next rush/chase
            }
        }

    }

    public void AdjustSpeed()
    {
        if(deceleration == 0) //if is not decelerating / decelerating done
        {
            if(currentSpeed < speed) //If currentSpeed is not reached maximum speed yet
            {
                currentSpeed += (speed / 5f) * Time.deltaTime; //adding speed
            }
            if(currentSpeed > speed) //If reach maximum speed
            {
                currentSpeed = speed; //no more adding
            }
        }
        else //decelerating required! value received from "Bullet" script
        {
            currentSpeed -= deceleration; //Decelerate one frame
            deceleration = 0; //reset to 0, so that it canbe added back again

            if (currentSpeed < 0) //I don't want enemy moving backwards of direction
            {
                currentSpeed = 0;
            }
        }
    }

    public void SetDeceleration(float deceleration) //When bullet touches enemy -> "Bullet" script
    {
        this.deceleration = deceleration;
    }
}
