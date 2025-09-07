using UnityEngine;

public class Pipeline : MonoBehaviour
{
    float time = 0;
    Vector2 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            time = 0;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            if(time >= 0.1f)
            {
                Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                time = 0;

                Debug.DrawLine(mousePos, newPos, Color.white, 5);
            }
            

        }
    }
}
