using UnityEngine;

public class AddVectors : MonoBehaviour
{

    public Transform R;
    public Transform B;

    void Start()
    {
        
    }


    void Update()
    {
        Vector2 origin = new Vector2(0f, 0f);

        // Click R and draw red line to R
        Vector2 red = R.position;
        if (Input.GetKey(KeyCode.R))
        {
            Debug.DrawLine(origin, red, Color.red);
        }
         // Click B and draw blue line to B
        Vector2 blue = B.position;
        if (Input.GetKey(KeyCode.B))
        {
            Debug.DrawLine(origin, blue, Color.blue);
        }

        // Draw a middle line of sum of rect R and B
        Vector2 both = red + blue;
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.B))
        {
            Debug.DrawLine(origin, both, Color.magenta);
        }


        if(Input.GetKeyDown(KeyCode.M))
        {
            //Calculate the magnitude of red and blue

            //magnitude = Mathf.Sqrt( xValue * xValue + yValue * yValue )

            float xValue = both.x;
            float yValue = both.y;
            float magnitude = Mathf.Sqrt(xValue * xValue + yValue * yValue);

            Debug.Log(magnitude);
        }
    }
}
