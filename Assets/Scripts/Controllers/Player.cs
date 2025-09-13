using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            //The first bomb prefab spawns between a distance of bombTrailSpacing below the player.
            Vector2 trailSpacing = new Vector2(0, bombTrailSpacing); //I created this vector to represent the distance.
            SpawnBombTrail(trailSpacing);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnBombOnRandomCorner();
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
        //I used player's initial pos minus the spacing(shift down) to represent the first position to spawn.
        Vector2 spawnTrailPos = (Vector2)transform.position - spacing;

        for (int i = 0; i < numberOfTrailBombs; i++)
        {
            //Spawn bomb. Repeating i times, depending by numberOfTrailBombs.
            Instantiate(bombPrefab, spawnTrailPos, Quaternion.identity);
            //The next time spawning, new bomb should shift down again by the same spacing
            //Othrewise they overlap at the same pos.
            spawnTrailPos.y -= spacing.y;
        }
    }

    public void SpawnBombOnRandomCorner()
    {
        int randomNum = Random.Range(0,3);

        if (randomNum == 0)
        {
            Instantiate(bombPrefab, transform.position + Vector3.up + Vector3.left, Quaternion.identity);
        }

        if (randomNum == 1)
        {
            Instantiate(bombPrefab, transform.position + Vector3.up + Vector3.right, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(bombPrefab, transform.position + Vector3.down + Vector3.left, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            Instantiate(bombPrefab, transform.position + Vector3.down + Vector3.right, Quaternion.identity);
        }

    }
}
