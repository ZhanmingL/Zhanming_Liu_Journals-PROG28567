using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed;

    bool charge = true; //Determine when enemy moves backward
    float timer = 0f; //Duration of moving backward

    private void Update()
    {
        enemyWait();
        enemyRush();
    }

    public void enemyWait() //Moving backwards of player, at beginning and when touches player
    {
        if (charge)
        {
            timer += Time.deltaTime;
            Vector3 deDirection = transform.position - player.transform.position; //Back direction
            transform.position += deDirection.normalized * speed / 4 * Time.deltaTime;
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
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (direction.magnitude < 1) //if distance between enemy & player < 1
            {
                charge = true; //Means enemy got and hurted player, moving backward again and getting ready fo next rush/chase
            }
        }

    }
}
