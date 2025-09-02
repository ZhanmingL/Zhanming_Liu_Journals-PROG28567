using UnityEngine;

public class AddVectors : MonoBehaviour
{

    public Transform R;
    public Transform B;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 start = new Vector2(0f, 0f);

        Vector2 red = R.position;

        Vector2 blue = B.position;

        Vector2 both = red + blue;


        if (Input.GetKey(KeyCode.R))
        {
            Debug.DrawLine(start, red, Color.red);
        }

        if (Input.GetKey(KeyCode.B))
        {
            Debug.DrawLine(start, blue, Color.blue);
        }

        if(Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.B))
        {
            Debug.DrawLine(start, both, Color.magenta);
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
