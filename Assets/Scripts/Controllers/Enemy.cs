using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed;

    bool charge = true;
    float timer = 0f;

    private void Update()
    {
        enemyWait();
        enemyRush();
    }

    public void enemyWait()
    {
        if (charge)
        {
            timer += Time.deltaTime;
            Vector3 deDirection = transform.position - player.transform.position;
            transform.position += deDirection.normalized * speed/2 * Time.deltaTime;
            if(timer > 3)
            {
                charge = false;
                timer = 0f;
            }
        }
        
    }

    public void enemyRush()
    {
        if(charge == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (direction.magnitude < 1)
            {
                charge = true;
            }
        }
        
    }
}
