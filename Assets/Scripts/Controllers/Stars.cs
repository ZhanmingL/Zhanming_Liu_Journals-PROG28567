using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < starTransforms.Count - 1; i++)
        {
            DrawConstellation(i, 0);
        }
    }


    public void DrawConstellation(int order, float length)
    {
        Debug.DrawLine(starTransforms[order].position, starTransforms[order + 1].position +
            ((starTransforms[order + 1].position - starTransforms[order + 1].position).normalized * length), Color.white, 3f);
        length += Time.deltaTime;
    }
}