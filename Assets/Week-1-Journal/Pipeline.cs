using UnityEngine;

public class Pipeline : MonoBehaviour
{
    float time = 0;

    Vector2 mousePos;
    Vector2 pastPos;

    bool firstDraw = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && firstDraw == true)
        {
            time = 0;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firstDraw = false;
        }

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;

            if (firstDraw == false)
            {
                Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Debug.DrawLine(mousePos, newPos, Color.white, 5);

                pastPos = newPos;

                time = 0;

                firstDraw = true;
            }

            if (firstDraw == true)
            {
                if (time >= 0.1f)
                {
                    Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Debug.DrawLine(pastPos, newPos, Color.white, 5);

                    pastPos = newPos;

                    time = 0;
                }
            }

        }
    }
}
