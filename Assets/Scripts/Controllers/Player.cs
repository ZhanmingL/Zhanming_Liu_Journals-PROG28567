using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public float bombTrailSpacing;
    public int numberOfTrailBombs;


    void Update()
    {
        float speed = 0.5f;

        Vector2 startPosition = transform.position; //player position
        Vector2 targetPosition = enemyTransform.position;
        Vector2 directionToMove = targetPosition - startPosition;

        if(Input.GetKey(KeyCode.M))
        {
            transform.position += (Vector3)directionToMove.normalized * speed;
        }


        if(Input.GetKeyDown(KeyCode.T))
        {
            Vector2 trailSpacing = new Vector2(0, bombTrailSpacing);
            SpawnBombTrail(trailSpacing);
        }



        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector2 offset = new Vector2(0,1);
            SpawnBombAtOffset(offset);
        }
    }

    public void SpawnBombAtOffset(Vector2 inOffset)
    {
        //Where are we going to spawn the bomb?
        Vector2 spawnPosition = (Vector2)transform.position + inOffset;

        //Spawning the bomb
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity); //Quaternion.identity = no rotation
    }

    public void SpawnBombTrail(Vector2 spacing)
    {
        Vector2 spawnTrailPos = (Vector2)transform.position - spacing;

        for (int i = 0; i < numberOfTrailBombs; i++)
        {
            
            Instantiate(bombPrefab, spawnTrailPos, Quaternion.identity);
            spawnTrailPos.y -= spacing.y;
        }
    }
}
