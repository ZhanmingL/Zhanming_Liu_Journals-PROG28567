using UnityEngine;

public class RedBlueAngles : MonoBehaviour
{
    public float red;
    public float blue;

    void Start()
    {
        
    }


    void Update()
    {
        float redRad = red * Mathf.Deg2Rad;
        float blueRad = blue * Mathf.Deg2Rad;

        Vector3 redPos = new Vector3(Mathf.Cos(redRad), Mathf.Sin(redRad)) * 1f;
        Vector3 bluePos = new Vector3(Mathf.Cos(blueRad), Mathf.Sin(blueRad)) * 1f;

        Debug.DrawLine(Vector3.zero, redPos, Color.red, 3f);
        Debug.DrawLine(Vector3.zero, bluePos, Color.blue, 3f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //float redDotBlue = redPos.x * bluePos.x + redPos.y * bluePos.y;
            float redDotBlue = Vector3.Dot(redPos, bluePos);

            Debug.Log("redDotBlue: " + redDotBlue.ToString());
        }
    }
}
