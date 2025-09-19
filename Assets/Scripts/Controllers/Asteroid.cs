using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float moveSpeed = 1f; //I don't want to enter 20 times in the inspector, so I make it private here.
    public float arrivalDistance;
    public float maxFloatDistance;

    Vector2 randomPoint;

    void Start()
    {
        GenerateRandomPoint();
    }


    void Update()
    {
        MoveAsteroid();
    }

    public void MoveAsteroid()
    {
        maxFloatDistance = Vector3.Distance(transform.position, randomPoint);
        

        Debug.Log(maxFloatDistance);

        if (maxFloatDistance > arrivalDistance)
        {
            Vector3 direction = (Vector3)randomPoint - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            GenerateRandomPoint();
        }
        
        
    }

    public void GenerateRandomPoint()
    {
        float x = Random.Range(0, Screen.width);
        float y = Random.Range(0, Screen.height);
        randomPoint = Camera.main.ScreenToWorldPoint(new Vector2(x, y));
    }
}
