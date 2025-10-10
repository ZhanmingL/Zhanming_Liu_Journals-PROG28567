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
        TowardingPlayer(speed * 3f);
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

    public void TowardingPlayer(float speed) //Enemy is always looking player
    {
        Vector3 direction = (player.position - transform.position).normalized; //Get direction of enemy facing player first
        float getDot = Vector3.Dot(transform.right, direction); //Check where is player, on the enemy's left/right side?
        float enemyAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.up.y, transform.up.x); //current angle of enemy
        float playerAngle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x); //player's angle
        float angleRemaining = Mathf.DeltaAngle(enemyAngle, playerAngle); //I got angle that need to rotate
        float changeInAngle;

        //If dot I got is positive
        //player is on the right half and rotate to the right
        if (getDot > 0)
        {
            //Rotating speed -> ddecreasing per frame due to rotating to the right
            changeInAngle = -speed * Time.deltaTime;

            //I don't want over-rotating, so let's check
            //If no more angle to rotate
            if (changeInAngle < angleRemaining)
            {
                //no rotating, keep towarding to the target
                transform.Rotate(0f, 0f, angleRemaining);
            }
            else //If it wouldn't over-rotating
            {
                //keep rotate
                transform.Rotate(0f, 0f, changeInAngle);
            }


        }
        else
        {
            //Move to the left
            changeInAngle = speed * Time.deltaTime;

            //Check as well -> no over-rotating
            if (changeInAngle > angleRemaining)
            {
                transform.Rotate(0f, 0f, angleRemaining);
            }
            else
            {
                transform.Rotate(0f, 0f, changeInAngle);
            }
        }
    }
}
