using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public GameObject powerUp;
    public Transform bombsTransform;

    List<int> randomAccount = new List<int> { 0, 1, 2, 3 }; //Four numbers hold four corners. Make this List's numbers random, therefore corners spawning will be random.
    bool canSpawn = true; //4 numbers exist at the beginning, so it's true when start.

    public float spacing;
    public int number;

    public float ratio; //ratio of distance moving towards enemy from player's position. I will edit it as 0, 0.5 or 1, so on.

    public float asteroidsRange = 2.5f; //Check how long the calculated magnitude is, less or = than 2.5 then drawLine.

    public float speed;

    //public float maxSpeed;
    public float maxSpeed = 10f;
    public float accelerationTime;
    public float decelerationTime;

    private float acceleration;
    private float deceleration;

    //private float time;

    public Vector3 velocity = new Vector3(0.5f, 0, 0);



    public float radius = 5f; //Radar radius
    public int circlePoint = 10;

    public int powerUpNum;



    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        SpawnPowerups(radius - 1, powerUpNum);
    }

    void Update()
    {

        EnemyRadar(radius, circlePoint); //Run radar every frame.

        float speed = 0.5f;

        Vector2 startPosition = transform.position; //player position
        Vector2 targetPosition = enemyTransform.position;
        Vector2 directionToMove = targetPosition - startPosition;

        PlayerMovement(speed);

        DetectAsteroids(asteroidsRange, asteroidTransforms); //Always check the distance between player and each asteroid.


        if (Input.GetKey(KeyCode.M))
        {
            transform.position += (Vector3)directionToMove.normalized * speed;
        }


        if (Input.GetKeyDown(KeyCode.T))
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
            Vector2 offset = new Vector2(0, 1);
            SpawnBombAtOffset(offset);
        }






        //acceleration
        //Vector2 direction = Vector2.zero;
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    direction += Vector2.right;
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    direction += Vector2.up;
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    direction -= Vector2.right;
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    direction -= Vector2.right;
        //}

        //direction = direction.normalized;
        //velocity += (Vector3)direction * acceleration * Time.deltaTime;
    }

    public void EnemyRadar(float radius, int circlePoints) //circlePoints - Ammounts of edges that I want, I want 10 in this case.
    {
        float averageAngle = 360 / circlePoints; //36 degrees per each angle

        float magnitude = Vector2.Distance(enemyTransform.position, transform.position); //check magnitude between player and enemy.

        Color radarColor = Color.green; //reference, change color when enemy within Radar.

        if(magnitude < radius) //within Radar
        {
            radarColor = Color.red; //change color to red
        }

        for (int i = 0; i < circlePoints;  i++) //Draw lines 10 times.
        {
            float angle2Radians = i * averageAngle * Mathf.Deg2Rad; //Transfer degree to radian, then get this radian's position
            float x = Mathf.Cos(angle2Radians);
            float y = Mathf.Sin(angle2Radians);
            Vector2 pointOne = new Vector2(x, y);
            pointOne *= radius; //Extend to what radius I want.
            pointOne += (Vector2)transform.position; //Follow palyer

            int index = i + 1; //Next point, get next degree
            float nextPointRadian = index * averageAngle * Mathf.Deg2Rad; //Get next point's position.
            float nextX = Mathf.Cos(nextPointRadian);
            float nextY = Mathf.Sin(nextPointRadian);
            Vector2 pointTwo = new Vector2(nextX, nextY);
            pointTwo *= radius;
            pointTwo += (Vector2)transform.position;

            Debug.DrawLine(pointOne, pointTwo, radarColor);

        }
    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        float averageDegree = 360 / numberOfPowerups;
        float averageRadian = Mathf.Deg2Rad * averageDegree;
        
        for(int i = 0; i < numberOfPowerups; i++)
        {
            float x = Mathf.Cos(averageRadian * i);
            float y = Mathf.Sin(averageRadian * i);
            Vector2 generatePoint = new Vector2(x, y) + (Vector2)transform.position;
            Instantiate(powerUp, generatePoint, Quaternion.identity);
        }
    }



    public void PlayerMovement(float speed)
    {
        //Player's movement
        Vector2 inputDirection = transform.position;
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        //a = v/s, v = d/s, d = vs, d = as`2

        if(inputDirection != Vector2.zero) //if wasd/arrows pressed/holding, the value is received
        {
            velocity += (Vector3)inputDirection.normalized * acceleration * Time.deltaTime; //moves towardd that direction
            if(velocity.magnitude > maxSpeed) //There is a maximum speed, reach that, then keep that speed, not increasing again.
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else //if key without input
        {
            if(velocity.magnitude > 0) //Should be velocity value to decrease.
            {
                Vector3 deVelocity = velocity.normalized * deceleration * Time.deltaTime;
                if(deVelocity.magnitude > velocity.magnitude) //if there is no more velocity to decrease
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


    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        Vector3 both1 = asteroidTransforms[0].position - transform.position; //From playerPos, get direction towards this asteroid.
        float x1 = both1.x;
        float y1 = both1.y;
        float magnitude1 = Mathf.Sqrt(both1.x * both1.x + both1.y * both1.y); //I have direction, so I can calculate the magnitude between player and this asteroid.
        Vector3 endPos1 = transform.position + both1.normalized * 2.5f; //I cannot draw a line between player and asteroid, this is not what I want.
        //As asteroid a direction, I want a line from player, towards this direction. Therefore, I get the end direction use by vector endPos.
        if (magnitude1 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos1, Color.green); //Draw green line between them.
        }

        //Repeating 20 times, I use this way is easy for thinking, but complecated in writting so many times.
        //Maybe a type of loop can solve this situation. But I have no time remaining, I will try later or next time.
        Vector3 both2 = asteroidTransforms[1].position - transform.position;
        float x2 = both2.x;
        float y2 = both2.y;
        float magnitude2 = Mathf.Sqrt(both2.x * both2.x + both2.y * both2.y);
        Vector3 endPos2 = transform.position + both2.normalized * 2.5f;
        if (magnitude2 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos2, Color.green);
        }

        Vector3 both3 = asteroidTransforms[2].position - transform.position;
        float x3 = both1.x;
        float y3 = both1.y;
        float magnitude3 = Mathf.Sqrt(both3.x * both3.x + both3.y * both3.y);
        Vector3 endPos3 = transform.position + both3.normalized * 2.5f;
        if (magnitude3 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos3, Color.green);
        }

        Vector3 both4 = asteroidTransforms[3].position - transform.position;
        float x4 = both1.x;
        float y4 = both1.y;
        float magnitude4 = Mathf.Sqrt(both4.x * both4.x + both4.y * both4.y);
        Vector3 endPos4 = transform.position + both4.normalized * 2.5f;
        if (magnitude4 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos4, Color.green);
        }

        Vector3 both5 = asteroidTransforms[4].position - transform.position;
        float x5 = both1.x;
        float y5 = both1.y;
        float magnitude5 = Mathf.Sqrt(both5.x * both5.x + both5.y * both5.y);
        Vector3 endPos5 = transform.position + both5.normalized * 2.5f;
        if (magnitude5 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos5, Color.green);
        }

        Vector3 both6 = asteroidTransforms[5].position - transform.position;
        float x6 = both6.x;
        float y6 = both6.y;
        float magnitude6 = Mathf.Sqrt(both6.x * both6.x + both6.y * both6.y);
        Vector3 endPos6 = transform.position + both6.normalized * 2.5f;
        if (magnitude6 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos6, Color.green);
        }

        Vector3 both7 = asteroidTransforms[6].position - transform.position;
        float x7 = both7.x;
        float y7 = both7.y;
        float magnitude7 = Mathf.Sqrt(both7.x * both7.x + both7.y * both7.y);
        Vector3 endPos7 = transform.position + both7.normalized * 2.5f;
        if (magnitude7 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos7, Color.green);
        }

        Vector3 both8 = asteroidTransforms[7].position - transform.position;
        float x8 = both8.x;
        float y8 = both8.y;
        float magnitude8 = Mathf.Sqrt(both8.x * both8.x + both8.y * both8.y);
        Vector3 endPos8 = transform.position + both8.normalized * 2.5f;
        if (magnitude8 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos8, Color.green);
        }

        Vector3 both9 = asteroidTransforms[8].position - transform.position;
        float x9 = both9.x;
        float y9 = both9.y;
        float magnitude9 = Mathf.Sqrt(both9.x * both9.x + both9.y * both9.y);
        Vector3 endPos9 = transform.position + both9.normalized * 2.5f;
        if (magnitude9 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos9, Color.green);
        }

        Vector3 both10 = asteroidTransforms[9].position - transform.position;
        float x10 = both10.x;
        float y10 = both10.y;
        float magnitude10 = Mathf.Sqrt(both10.x * both10.x + both10.y * both10.y);
        Vector3 endPos10 = transform.position + both10.normalized * 2.5f;
        if (magnitude10 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos10, Color.green);
        }

        Vector3 both11 = asteroidTransforms[10].position - transform.position;
        float x11 = both11.x;
        float y11 = both11.y;
        float magnitude11 = Mathf.Sqrt(both11.x * both11.x + both11.y * both11.y);
        Vector3 endPos11 = transform.position + both11.normalized * 2.5f;
        if (magnitude11 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos11, Color.green);
        }

        Vector3 both12 = asteroidTransforms[11].position - transform.position;
        float x12 = both12.x;
        float y12 = both12.y;
        float magnitude12 = Mathf.Sqrt(both12.x * both12.x + both12.y * both12.y);
        Vector3 endPos12 = transform.position + both12.normalized * 2.5f;
        if (magnitude12 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos12, Color.green);
        }

        Vector3 both13 = asteroidTransforms[12].position - transform.position;
        float x13 = both13.x;
        float y13 = both13.y;
        float magnitude13 = Mathf.Sqrt(both13.x * both13.x + both13.y * both13.y);
        Vector3 endPos13 = transform.position + both13.normalized * 2.5f;
        if (magnitude13 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos13, Color.green);
        }

        Vector3 both14 = asteroidTransforms[13].position - transform.position;
        float x14 = both14.x;
        float y14 = both14.y;
        float magnitude14 = Mathf.Sqrt(both14.x * both14.x + both14.y * both14.y);
        Vector3 endPos14 = transform.position + both14.normalized * 2.5f;
        if (magnitude14 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos14, Color.green);
        }

        Vector3 both15 = asteroidTransforms[14].position - transform.position;
        float x15 = both1.x;
        float y15 = both1.y;
        float magnitude15 = Mathf.Sqrt(both15.x * both15.x + both15.y * both15.y);
        Vector3 endPos15 = transform.position + both15.normalized * 2.5f;
        if (magnitude15 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos15, Color.green);
        }

        Vector3 both16 = asteroidTransforms[15].position - transform.position;
        float x16 = both1.x;
        float y16 = both1.y;
        float magnitude16 = Mathf.Sqrt(both16.x * both16.x + both16.y * both16.y);
        Vector3 endPos16 = transform.position + both16.normalized * 2.5f;
        if (magnitude16 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos16, Color.green);
        }

        Vector3 both17 = asteroidTransforms[16].position - transform.position;
        float x17 = both17.x;
        float y17 = both17.y;
        float magnitude17 = Mathf.Sqrt(both17.x * both17.x + both17.y * both17.y);
        Vector3 endPos17 = transform.position + both17.normalized * 2.5f;
        if (magnitude17 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos17, Color.green);
        }

        Vector3 both18 = asteroidTransforms[17].position - transform.position;
        float x18 = both18.x;
        float y18 = both18.y;
        float magnitude18 = Mathf.Sqrt(both18.x * both18.x + both18.y * both18.y);
        Vector3 endPos18 = transform.position + both18.normalized * 2.5f;
        if (magnitude18 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos18, Color.green);
        }

        Vector3 both19 = asteroidTransforms[18].position - transform.position;
        float x19 = both19.x;
        float y19 = both19.y;
        float magnitude19 = Mathf.Sqrt(both19.x * both19.x + both19.y * both19.y);
        Vector3 endPos19 = transform.position + both19.normalized * 2.5f;
        if (magnitude19 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos19, Color.green);
        }

        Vector3 both20 = asteroidTransforms[19].position - transform.position;
        float x20 = both20.x;
        float y20 = both20.y;
        float magnitude20 = Mathf.Sqrt(both20.x * both20.x + both20.y * both20.y);
        Vector3 endPos20 = transform.position + both20.normalized * 2.5f;
        if (magnitude20 <= inMaxRange)
        {
            Debug.DrawLine(transform.position, endPos20, Color.green);
        }

    }

}
