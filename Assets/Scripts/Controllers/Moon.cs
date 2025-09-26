using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    public float radius;
    public float speed;

    float initialDegree; //initial orbitting degree, 0 degree at beginning

    void Start()
    {
        
    }


    void Update()
    {
        OrbitalMotion(radius, speed, planetTransform);
    }


    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        //float initialDegree += speed * Time.deltaTime;
        //In terms of degree changes, radian changes, therefore point is moving around a whole radian > a circle
        initialDegree += speed * Time.deltaTime;
        float initialRadian = initialDegree * Mathf.Deg2Rad;
        float x = Mathf.Cos(initialRadian);
        float y = Mathf.Sin(initialRadian);
        Vector2 orbitPos = new Vector2(x, y); //Calculated point > radian adding from 0 to 2 π radians > so orbitting
        orbitPos *= radius; //radius
        orbitPos += (Vector2)planetTransform.position; //It's for moon's motion
        transform.position = orbitPos;

    }
}
