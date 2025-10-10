using UnityEngine;

public class AnotherTurret : MonoBehaviour
{
    public Transform playerPos;

    float turretDegree = 180f;
    float turretRadian;

    public float turretRadius;
    public float turretSpeed;

    void Start()
    {

    }


    void Update()
    {
        Orbiting(turretRadius, turretSpeed);

    }

    public void Orbiting(float radius, float speed)
    {
        turretDegree += speed * Time.deltaTime;
        turretRadian = Mathf.Deg2Rad * turretDegree;
        float x = Mathf.Cos(turretRadian);
        float y = Mathf.Sin(turretRadian);
        Vector3 orbitPos = new Vector2(x, y) * radius;
        orbitPos += playerPos.position;
        transform.position = orbitPos;
    }
}