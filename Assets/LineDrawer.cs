using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Hello, World!");

        Vector2 start = new Vector2(0f, 0f);

        Vector2 end = new Vector2(0f, 1f);

        Debug.DrawLine(start, end, Color.yellow);

        end = new Vector2(3f, -2f);

        Debug.DrawLine(start, end, Color.grey);
    }
}
