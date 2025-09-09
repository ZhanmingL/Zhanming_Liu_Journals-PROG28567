using UnityEngine;

public class MathFunctions : MonoBehaviour
{
    public static float healthPoints = 0f;


    void Start()
    {
        Vector2 position = transform.position;

        float Magnitude = GetMagnitude(position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetMagnitude(Vector2 position)
    {
        return Mathf.Sqrt(position.x * position.x + position.y * position.y);
    }

    public static void DrawSquare(Vector2 position, float size, Color color, float duration)
    {
        Vector2 topLeftPoint = position + Vector2.up * size / 2 + Vector2.left * size / 2;
        Vector2 topRightPoint = position + Vector2.up * size / 2 + Vector2.right * size / 2;
        Vector2 bottomLeftPoint = position + Vector2.down * size / 2 + Vector2.left * size / 2;
        Vector2 bottomRightPoint = position + Vector2.down * size / 2 + Vector2.right * size / 2;

        Debug.DrawLine(topLeftPoint, topRightPoint, color, duration);
        Debug.DrawLine(topRightPoint, bottomRightPoint, color, duration);
        Debug.DrawLine(bottomRightPoint, bottomLeftPoint, color, duration);
        Debug.DrawLine(bottomLeftPoint, topLeftPoint, color, duration);
    }
}
