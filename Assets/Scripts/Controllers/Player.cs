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

    List<int> randomAccount = new List<int> { 0, 1, 2, 3 }; //Four numbers hold four corners. Make this List's numbers random, therefore corners spawning will be random.
    bool canSpawn = true; //4 numbers exist at the beginning, so it's true when start.

    public float spacing;
    public int number;

    public float ratio; //ratio of distance moving towards enemy from player's position. I will edit it as 0, 0.5 or 1, so on.

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
            SpawnBombTrail(spacing, number);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (canSpawn == true) //if the List still have value in it, empty corner can spawn. Otherwise spawning is forbidden.
            {
                SpawnBombOnRandomCorner(1); //Randomly spawn bombs at corners. The distance between player and each bomb is 1 (1 means the straight-line distance between them).
            }
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            WarpPlayer(enemyTransform, ratio);
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

    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        //I used player's initial pos minus the spacing(shift down) to represent the first position to spawn.
        Vector2 spawnTrailPos = (Vector2)transform.position;
        spawnTrailPos.y -= inBombSpacing;

        for (int i = 0; i < inNumberOfBombs; i++)
        {
            //Spawn bomb. Repeating i times, depending by numberOfTrailBombs.
            Instantiate(bombPrefab, spawnTrailPos, Quaternion.identity);
            //The next time spawning, new bomb should shift down again by the same spacing
            //Othrewise they overlap at the same pos.
            spawnTrailPos.y -= inBombSpacing;
        }
    }

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector3 direction = Vector3.zero;

        int randomNum = randomAccount[Random.Range(0, randomAccount.Count)]; //Randomly select a value from my List.

        if (randomNum == 0)
        {
            //If selected 0, spawn a bomb at top left corner.
            //Instantiate(bombPrefab, transform.position + Vector3.up + Vector3.left, Quaternion.identity);

            //Didn't solve by float inDistance.
            //inDistance = Mathf.Sqrt(Vector3.up.x * Vector3.up.x + Vector3.left.x * Vector3.left.x) + Mathf.Sqrt(Vector3.up.y * Vector3.up.y + Vector3.left.y * Vector3.left.y);
            //Instantiate(bombPrefab, new Vector2(transform.position.x + inDistance, transform.position.y + inDistance), Quaternion.identity);

            direction = Vector3.up + Vector3.left;

            randomAccount.Remove(0); //remove from list!
        }

        if (randomNum == 1)
        {
            //If 1, top right corner.
            //Instantiate(bombPrefab, transform.position + Vector3.up + Vector3.right, Quaternion.identity);

            direction = Vector3.up + Vector3.right;

            randomAccount.Remove(1); //remove from list!!
        }

        if (randomNum == 2)
        {
            //2 bottom left
            //Instantiate(bombPrefab, transform.position + Vector3.down + Vector3.left, Quaternion.identity);

            direction = Vector3.down + Vector3.left;

            randomAccount.Remove(2); //remove from list!!!
        }

        if (randomNum == 3)
        {
            //3 bottom right
            //Instantiate(bombPrefab, transform.position + Vector3.down + Vector3.right, Quaternion.identity);

            direction = Vector3.down + Vector3.right;

            randomAccount.Remove(3); //remove from list!!!!!!!!!
        }


        Vector3 spawnPos = transform.position + direction * inDistance; //Calculate added position: one of the corner's pos
        Instantiate(bombPrefab, spawnPos, Quaternion.identity); //Spawn


        if (randomAccount.Count == 0) //if all values from my list are deleted
        {
            canSpawn = false; //turn this bool to false, therefore no more bomb will be allowed to spawn.
        }

    }

    public void WarpPlayer(Transform target, float ratio)
    {
        transform.position = Vector3.Lerp(transform.position, target.position, ratio); //From player's pos to enemy's pos, ratio for distance.
    }
}
