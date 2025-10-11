using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Acceleration counting
    Vector3 velocity;
    float acceleration = 1f;

    //Access enemy's position -> used for direction
    public Transform enemy;


    void Update()
    {
        //If return true
        if (CheckDistance())
        {
            enemy.GetComponent<SpaceEnemy>().SetDeceleration(1f); //Deceleration acting on SpaceEnemy script -> decelerate enemy one frame
            Destroy(gameObject); //Bomb gets out here!
        }
        //else

        //Change direction towards enemy
        Vector3 direction = (enemy.position - transform.position).normalized;

        //Increase velocity -> acceleration
        float magnitude = velocity.magnitude; //current moving magnitude
        magnitude += acceleration * Time.deltaTime; //magnitude plus one per frame
        velocity = direction * magnitude; //plused magnitude towards direction is velocity -> accelerating

        transform.position += velocity * Time.deltaTime;
    }

    public void SetTarget(Transform myEnemy)
    {
        enemy = myEnemy; //Enemy reference -> Spaceplayer -> spawned
    }

    public void SetVelocity(Vector3 initialVelocity)
    {
        velocity = initialVelocity; //Set initial velocity (from smallest) -> spawned
    }

    public bool CheckDistance() //Check distance magnitude between bullet and enemy
    {
        float distance = Vector3.Distance(enemy.position, transform.position); //get magnitude
        if(distance < 0.1f) //if within 0.1, means actually touched
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
