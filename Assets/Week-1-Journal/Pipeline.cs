using UnityEngine;

public class Pipeline : MonoBehaviour
{
    float time = 0; //Timer

    Vector2 mousePos; //Initial point when mouse clicked
    Vector2 pastPos; //new point's previous point
    Vector2 counting; //Calculate generated point's vectors

    bool firstDraw = true; //Check if it's clicking mouse or holding mouse.
    //Helps check first frame; Helps check the connecting of first point and second point.

    void Update()
    {
        //Check the first click of the mouse: first frame when mouse clicked
        if (Input.GetMouseButtonDown(0) && firstDraw == true)
        {
            time = 0;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firstDraw = false;
        }

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            //The first connection (original mouse position and first generated point)
            if (firstDraw == false)
            {
                Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Debug.DrawLine(mousePos, newPos, Color.white, 60);
                counting += mousePos + newPos; //Calculate length.

                pastPos = newPos; //Change this point to NEXT NEW POINT's previous point

                time = 0; //Re-load the timer

                firstDraw = true; //Turn it to true so that it will not run this if loop anymore.
            }

            if (firstDraw == true) //Check all generated points from 2nd point.
            {
                if (time >= 0.1f)
                {
                    Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Debug.DrawLine(pastPos, newPos, Color.white, 60);
                    counting += pastPos + newPos;

                    pastPos = newPos; //The same meaning, change new point to next new one's previous one.

                    time = 0;
                }
            }

            if (Input.GetMouseButtonUp(0))
            { //When up my mouse, count sum of total length.
                float magnitude = Mathf.Sqrt(counting.x * counting.x + counting.y * counting.y);
                Debug.Log(magnitude);
            }
        }
    }
}
