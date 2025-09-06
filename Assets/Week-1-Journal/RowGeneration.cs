using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    public Button generate;
    public TMP_InputField squaresNumInput;

    float squareLeftTop; //Each spawned square's top left point's x value. Used for getting other 3 points as well.
    int length = 2; //This time is full side length, not half.

    void Update()
    {

        

    }

    public void GenerateRow()
    {
        int rowNumber = int.Parse(squaresNumInput.text);

        Vector2 ScreenLeftPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        
        for(int i = 0; i < rowNumber; i++)
        {
            squareLeftTop = ScreenLeftPos.x + i;

            Vector2 leftTop = new Vector2(squareLeftTop, ScreenLeftPos.y);
            Vector2 rightTop = new Vector2(squareLeftTop + length, ScreenLeftPos.y);
            Vector2 leftBottom = new Vector2(squareLeftTop, ScreenLeftPos.y - length/2);
            Vector2 rightBottom = new Vector2(squareLeftTop + length, ScreenLeftPos.y - length/2);

            Debug.DrawLine(leftTop, rightTop, Color.white, 5);
            Debug.DrawLine(rightTop, rightBottom, Color.white, 5);
            Debug.DrawLine(rightBottom, leftBottom, Color.white, 5);
            Debug.DrawLine(leftBottom, leftTop, Color.white, 5);
        }
    }
}
