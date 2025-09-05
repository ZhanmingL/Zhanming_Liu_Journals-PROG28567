using UnityEngine;

public class SquareSpawner : MonoBehaviour
{

    void Update()
    {
        int length = 2;

        //Get the middle point of square spawning (mousePos)
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Get four points around mousePos that combine a quare
        //I use 5 as the value of each length of the square
        Vector2 leftTop = new Vector2(mousePos.x - length, mousePos.y + length);
        Vector2 rightTop = new Vector2(mousePos.x + length, mousePos.y + length);
        Vector2 leftBottom = new Vector2(mousePos.x - length, mousePos.y - length);
        Vector2 rightBottom = new Vector2(mousePos.x + length, mousePos.y - length);

        //Spawn squares
        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawLine(leftTop, rightTop, Color.white, 5);
            Debug.DrawLine(rightTop, rightBottom, Color.white, 5);
            Debug.DrawLine(rightBottom, leftBottom, Color.white, 5);
            Debug.DrawLine(leftBottom, leftTop, Color.white, 5);
        }
    }
}
