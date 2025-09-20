using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    [Range(0, 0.5f)]
    public float t;
    public AnimationCurve curve;
    void Update()
    {
        DrawConstellation(0);

        //for (int i = 0; i < starTransforms.Count - 1; i++)
        //{
        //   DrawConstellation(i, 0);
        //}
    }


    public void DrawConstellation(int i)
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

        }

        Debug.DrawLine(starTransforms[i].position, addLength, Color.white);
    }
}