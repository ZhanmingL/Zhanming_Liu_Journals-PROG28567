using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    float drawingTime = 1;

    int i = 0;
    //[Range(0, 1)]
    public float t;

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

        if (t < drawingTime)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
            i++;
        }

        if(i == starTransforms.Count - 1)
        {
            i = 0;
        }

        Debug.DrawLine(starTransforms[i].position, addLength, Color.white);
    }
}