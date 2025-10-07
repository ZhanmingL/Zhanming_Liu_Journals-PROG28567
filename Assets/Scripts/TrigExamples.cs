using UnityEngine;

public class TrigExamples : MonoBehaviour
{
    public float currentAngle = 90f;


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            Vector3 convertedVector = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * 1f;

            Debug.DrawLine(transform.position, transform.position + convertedVector, Color.red, 3f);

            float reconvertedAngle = Mathf.Atan2(convertedVector.y, convertedVector.x);

            Debug.Log("Reconverted angle: " + reconvertedAngle.ToString());

        }
    }
}