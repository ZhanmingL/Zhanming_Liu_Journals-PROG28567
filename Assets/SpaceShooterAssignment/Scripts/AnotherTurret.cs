using UnityEngine;

public class AnotherTurret : MonoBehaviour
{
    public Transform playerPos;
    public Transform enemyPos;

    //Only difference between this script with another one
    float turretDegree = 180f;
    float turretRadian;

    public float turretRadius;
    public float turretSpeed;

    void Start()
    {

    }


    void Update()
    {
        Orbiting(turretRadius, turretSpeed);
        TowardingEnemy(turretSpeed);

    }

    public void Orbiting(float radius, float speed)
    {
        turretDegree += speed * Time.deltaTime; //Adding from 180 to 360, -180 degrees, then back to 180, and adding again......
        turretRadian = Mathf.Deg2Rad * turretDegree; //I want radian
        float x = Mathf.Cos(turretRadian);
        float y = Mathf.Sin(turretRadian); //Get x and y of target position on the circle
        Vector3 orbitPos = new Vector2(x, y) * radius; //position on the circle and give it a radius
        orbitPos += playerPos.position; //That posiiton is based on player's position
        transform.position = orbitPos; //orbiting
    }

    public void TowardingEnemy(float speed)
    {
        Vector3 direction = (enemyPos.position - transform.position).normalized; //Get direction of bullet facing enemy first
        float getDot = Vector3.Dot(-transform.right, direction); //Turret barrel on the left so I use Transform.left this time
        float turretAngle = Mathf.Rad2Deg * Mathf.Atan2(-transform.right.y, -transform.right.x); //current angle keep same direction with Dot
        float enemyAngle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x); //enemy's angle
        float angleRemaining = Mathf.DeltaAngle(turretAngle, enemyAngle); //I got angle that need to rotate
        float changeInAngle;

        //If dot I got is positive
        //The object is on the left half and rotate to the left
        if (getDot < 0)
        {
            //Rotating left hand part if dot < 0
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
            //Move to the right
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