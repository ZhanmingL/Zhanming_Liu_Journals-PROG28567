using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float moveSpeed = 1f; //I don't want to enter 20 times in the inspector, so I make it private here.
    public float arrivalDistance; //check value that asteroid arrives at randompoint -> 0.2 in inspector.
    public float maxFloatDistance; //distance between player and enemy - magnitude -> but I use distance to calculate in this case.

    Vector2 randomPoint; //generate a new random point that asteroid is moving to

    void Start()
    {
        GenerateRandomPoint(); //get that random point when starts
    }


    void Update()
    {
        MoveAsteroid(); //asteroid always move
    }

    public void MoveAsteroid()
    {
        maxFloatDistance = Vector3.Distance(transform.position, randomPoint); //magnitude distance between asteroid and that random point
        Debug.Log(maxFloatDistance);

        if (maxFloatDistance > arrivalDistance)
        { //if bigger than 0.2 which means not arrived at random point yet, keeping moving asteroid
            Vector3 direction = (Vector3)randomPoint - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
        else
        { //otherwise generate a new random point, so asteroid will go to new point.
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
