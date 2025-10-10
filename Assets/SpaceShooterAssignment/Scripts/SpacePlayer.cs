using UnityEngine;
using UnityEngine.UIElements;

public class SpacePlayer : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bulletPrefab;

    //player's acceleration
    public float maxSpeed = 10f;
    public float accelerationTime;
    public float decelerationTime;

    private float acceleration;
    private float deceleration;

    public Vector3 velocity = new Vector3(0.5f, 0, 0);

    GameObject newBullet;
    bool canShoot = false;
    bool shoot = true;

    //bullet's acceleration
    public float bulletMaxSpeed = 10f;
    public float bulletAccelerationTime;
    public float bulletDecelerationTime;

    public Transform turret1;
    public Transform turret2;
    float turretNumber = 0f; //Shooting bullet, the one that spawn bullet

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;

    }

    void Update()
    {
        float speed = 0.5f;
        PlayerMovement(speed);

        //The logic - within magnitude - allow shoot - press button to shoot - update bullet moving
        checkMagnitude();
    }

    //if distance between enemy & player is within the allowing magnitude
    public void checkMagnitude()
    {
        float magnitude = Vector2.Distance(transform.position, enemyTransform.position);
        if(magnitude < 5)
        {
            Debug.DrawLine(transform.position, enemyTransform.position, Color.green);
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    //Allow shoot, allow press the button
    public void shootButtonDown()
    {
        if(turretNumber == 0)
        {
            if (canShoot)
            {
                //Spawn bomb and initialize their direction and velocity
                newBullet = Instantiate(bulletPrefab, turret1.position, Quaternion.identity);
                newBullet.GetComponent<Bullet>().SetTarget(enemyTransform);
                newBullet.GetComponent<Bullet>().SetVelocity((enemyTransform.position - transform.position).normalized * 3f); //Initial velocity includes both direction & speed
            }
        }
        else
        {
            if (canShoot)
            {
                newBullet = Instantiate(bulletPrefab, turret2.position, Quaternion.identity);
                newBullet.GetComponent<Bullet>().SetTarget(enemyTransform);
                newBullet.GetComponent<Bullet>().SetVelocity((enemyTransform.position - transform.position).normalized * 3f);
            }
        }

        turretNumber++;
        if(turretNumber == 2)
        {
            turretNumber = 0f;
        }
        
    }



    public void PlayerMovement(float speed)
    {
        //Player's basic movement
        Vector2 inputDirection = transform.position;
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        if (inputDirection != Vector2.zero)
        {
            velocity += (Vector3)inputDirection.normalized * acceleration * Time.deltaTime; //moves towardd that direction
            if (velocity.magnitude > maxSpeed) //There is a maximum speed, reach that, then keep that speed, not increasing again.
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else //if key without input
        {
            if (velocity.magnitude > 0) //Should be velocity value to decrease.
            {
                Vector3 deVelocity = velocity.normalized * deceleration * Time.deltaTime;
                if (deVelocity.magnitude > velocity.magnitude) //if there is no more velocity to decrease
                {
                    velocity = Vector3.zero; //player stops
                }
                else
                {
                    velocity -= deVelocity; //otherwise keep decreasing.
                }
            }
        }

        transform.position += velocity * Time.deltaTime;
    }


}
