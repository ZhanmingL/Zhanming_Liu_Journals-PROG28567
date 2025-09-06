using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    public Transform mouseSquare; //Semi-square that follows the mouse

    float length = 0; //Value of length of Semi-Square and use for calculating length of new spawned squares

    private void Start()
    {
        length = mouseSquare.localScale.x / 2; //I found that spawned square's length is twice longer than semi-square's length,
                                               //so I devide it by two, now the lengths are matched!
    }


    void Update()
    {
        //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Input-mouseScrollDelta.html Reference of mouse scroll
        length += Input.mouseScrollDelta.y * Time.deltaTime; //Increase length value so new spawned squares calculate and spawn as new length.

        mouseSquare.localScale = new Vector2(length * 2, length * 2); //Because I found that I set length's value is half of semi-square's value.
                                                                      //Therefore I time both x and y values by 2, the issue solved. Now their lengths are equal.
        
        //Get the middle point of square spawning (mousePos)
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseSquare.position = mousePos; //Semi-Square follows mouse

        //Get four points around mousePos that combine a quare
        //I use 5 as the value of each length of the square
        Vector2 leftTop = new Vector2(mousePos.x - length, mousePos.y + length);
        Vector2 rightTop = new Vector2(mousePos.x + length, mousePos.y + length);
        Vector2 leftBottom = new Vector2(mousePos.x - length, mousePos.y - length);
        Vector2 rightBottom = new Vector2(mousePos.x + length, mousePos.y - length);

        //Spawn square
        if (Input.GetMouseButtonDown(0))
        {
            //Four lines generates by four points
            Debug.DrawLine(leftTop, rightTop, Color.white, 5);
            Debug.DrawLine(rightTop, rightBottom, Color.white, 5);
            Debug.DrawLine(rightBottom, leftBottom, Color.white, 5);
            Debug.DrawLine(leftBottom, leftTop, Color.white, 5);
        }
    }
}
