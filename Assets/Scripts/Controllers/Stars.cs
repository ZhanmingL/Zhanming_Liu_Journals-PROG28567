using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    float drawingTime = 1;

    int i = 0; //order from List
    //[Range(0, 1)]
    public float t; //Draw line time counting.

    void Update()
    {
        DrawConstellation();

        //for (int i = 0; i < starTransforms.Count - 1; i++)
        //{
        //   DrawConstellation(i, 0);
        //}
    }


    public void DrawConstellation()
    {
        //Debug.DrawLine(starTransforms[order].position, (starTransforms[order + 1].position +
        //   ((starTransforms[order + 1].position - starTransforms[order + 1].position).normalized) * length), Color.white, 3f);
        
        Vector3 addLength = Vector3.Lerp(starTransforms[i].position, starTransforms[i + 1].position, t);

        if (t < drawingTime) //If it haven't finished drawing line to each target point
        {
            t += Time.deltaTime; //so keep drawing
        }
        else
        {   //otherwise reset timer to 0, and draw the next phase of line.
            t = 0;
            i++;
        }

        if(i == starTransforms.Count - 1) //if all stars from List has been drawn,(i is counted from 0, so Count-1)
        {
            i = 0; //Reset i to one so that draw again from beginning.
        }

        Debug.DrawLine(starTransforms[i].position, addLength, Color.white);
    }
}