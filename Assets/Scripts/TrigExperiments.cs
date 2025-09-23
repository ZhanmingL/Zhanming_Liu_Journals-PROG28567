using System.Collections.Generic;
using UnityEngine;

public class TrigExperiments : MonoBehaviour
{
    public List<float> angles = new List<float>() {36, 36 * 2, 36 * 3, 36 * 4, 36 * 5, 36 * 6, 36 * 7, 36 * 8, 36 * 9, 360};
    public float radius;
    public Vector3 circlePosition;

    private int currentAngleIndex = 0;


    void Start()
    {
        
    }


    void Update()
    {
        float angleInDegrees = angles[currentAngleIndex];
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        //float angleInDegreesAgain = angleInRadians * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);


        float convertedAngle = Mathf.Atan2(y, x);
        Debug.Log(convertedAngle);


        //float angleFromInverseFunction = Mathf.Deg2Rad * Mathf.Asin(y / 1);
        //Debug.Log(angleFromInverseFunction);

        Vector3 pointOnCircle = new Vector3(x, y, 0) * radius + circlePosition;
        Debug.DrawLine(circlePosition, pointOnCircle, Color.white);




        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentAngleIndex++;

            if(currentAngleIndex >= angles.Count)
            {
                currentAngleIndex = 0;
            }
        }
    }


}
