/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WordsGrid : MonoBehaviour
{
    public LevelData currentLevelData;
    public GameObject gridSquarePrefab;
    public AlphabetData alphabetData;

    public float squareOffset = 0;
    public float topPos;

    private List<GameObject> _squaresList = new List<GameObject>();

    void Start()
    {
        SpawnGridSquares();
        SetSquarePosition();
    }

    private void SetSquarePosition()
    {
        var squareRect = _squaresList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squaresList[0].GetComponent<Transform>();

        var offset = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f
        };
        
        var startPos = GetFirstSquarePosition();

        ///I do not understand this part
        int columnNumber = 0;
        int rowNumber = 0;
        foreach (var square in _squaresList)
        {
            if (rowNumber + 1 > currentLevelData.selectedGameBoard.rows)
            {
                columnNumber++;
                rowNumber = 0;
            }
            
            var positionX = startPos.x + offset.x * columnNumber;
            var positionY = startPos.y - offset.y * rowNumber;

            square.GetComponent<Transform>().position = new Vector3(positionX, positionY, 0);
            rowNumber++;
        }
        
    }

    private Vector2 GetFirstSquarePosition()
    {
        var StartPosition = new Vector2(0f, transform.position.y);
        var squareRect = _squaresList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squaresList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);


        squareSize.x = squareRect.width * squareTransform.localScale.x;
        squareSize.y = squareRect.height * squareTransform.localScale.y;

        var miwidthPosition = (((currentLevelData.selectedGameBoard.columns - 1) * squareSize.x) / 2) * 0.01f;
        var midWidthHeight = (((currentLevelData.selectedGameBoard.rows - 1) * squareSize.y) / 2) * 0.01f;

        StartPosition.x = (miwidthPosition != 0) ? miwidthPosition * -1 : miwidthPosition;
        StartPosition.y += midWidthHeight;


        return StartPosition;
    }

    private void SpawnGridSquares()
    {
        if (currentLevelData != null)
        {
            var squareScale = GetSquareScale(new Vector3(1.5f, 1.5f, 0));
            
            foreach (var squares in currentLevelData.selectedGameBoard.board)
            {
                foreach (var squareLetter in squares.row)
                {
                    // I do not understand this line
                    //lambda
                    var normalLetterData = alphabetData.alphabetWhite.Find(data => data.letterName == squareLetter);
                    var draggedLetterData = alphabetData.alphabetBlue.Find(data => data.letterName == squareLetter);
                    var correctLetterData = alphabetData.alphabetGreen.Find(data => data.letterName == squareLetter);
                    var wrongLetterData = alphabetData.alphabetRed.Find(data => data.letterName == squareLetter);

                    if (normalLetterData.letterImage == null)
                    {
                        Debug.LogError(
                            "All Fields in your Array should have some letters. Press Fill up the random button in your board data to add random letter.Letter: " +
                            squareLetter);
                        
                        #if UNITY_EDITOR
                        if (UnityEditor.EditorApplication.isPlaying)
                        {
                            UnityEditor.EditorApplication.isPlaying = false;
                        }

                        #endif
                    }
                    else
                    {
                        _squaresList.Add(Instantiate(gridSquarePrefab));
                        _squaresList[_squaresList.Count - 1].GetComponent<GridSquare>().SetSprite(normalLetterData,
                            draggedLetterData, correctLetterData, wrongLetterData);
                        _squaresList[_squaresList.Count - 1].transform.SetParent(this.transform);
                        _squaresList[_squaresList.Count - 1].GetComponent<Transform>().position = new Vector3(0, 0, 0);
                        _squaresList[_squaresList.Count - 1].transform.localScale = squareScale;
                    }
                }
            }
        }
    }
    
    //for getting the size of the board from boardData
    private Vector3 GetSquareScale(Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        var adjustment = 0.01f;
        
        //if the board size is bigger than the screen size and therefore should be scaled smaller
        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if (finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;
            }
        }

        return finalScale;
    }
    
    //for checking if there is a need to scale down or not
    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0, 0);
        var startPos = new Vector2(0, 0);

        squareSize.x = (squareRect.width * targetScale.x) + squareOffset;
        squareSize.y = (squareRect.height * targetScale.y) + squareOffset;

        var midWidthPos = ((currentLevelData.selectedGameBoard.columns * squareSize.x) / 2) * 0.01f;
        var midHeightPos = ((currentLevelData.selectedGameBoard.rows * squareSize.y) / 2) * 0.01f;

        startPos.x = (midWidthPos != 0) ? midWidthPos * -1 : midWidthPos;
        startPos.y = midHeightPos;

        return startPos.x < GetHalfScreenWidth() * -1 || startPos.y > topPos;
    }

    private float GetHalfScreenWidth()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = (1.7f * height) * Screen.width / Screen.height;
        return width / 2;
    }
}
*/
