using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    //claim
    public Button generate;
    public TMP_InputField squaresNumInput;

    float squareLeftTop; //Each spawned square's top left point's x value. Used for getting other 3 points as well.
    int length = 2; //This time is full side length, not half. Each side length of square is 2.


    //When button clicks
    public void GenerateRow()
    {
        int rowNumber = int.Parse(squaresNumInput.text); //Transfer Input text value to int variable value.

        Debug.Log(rowNumber); //Check whether squareNumInput's value is valid or not.

        Vector2 ScreenLeftPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        //World's coordinate stands for left point, y value is in the middle.
        
        for(int i = 0; i < rowNumber; i++)
        {
            squareLeftTop = ScreenLeftPos.x + i; //Each new spawned square's topleft x value moves i unit to the right. So it achieves side by side.

            //In each of my for loop, get four points of each square.
            Vector2 leftTop = new Vector2(squareLeftTop, ScreenLeftPos.y);
            Vector2 rightTop = new Vector2(squareLeftTop + length, ScreenLeftPos.y);
            Vector2 leftBottom = new Vector2(squareLeftTop, ScreenLeftPos.y - length/2); //use half of length to get bottom points' y values.
            Vector2 rightBottom = new Vector2(squareLeftTop + length, ScreenLeftPos.y - length/2);
            //Draw squares and keep 5 seconds.
            Debug.DrawLine(leftTop, rightTop, Color.white, 5);
            Debug.DrawLine(rightTop, rightBottom, Color.white, 5);
            Debug.DrawLine(rightBottom, leftBottom, Color.white, 5);
            Debug.DrawLine(leftBottom, leftTop, Color.white, 5);
        }
    }
}
