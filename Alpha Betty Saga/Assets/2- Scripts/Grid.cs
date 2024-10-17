using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Grid : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public GameObject mapTilePrefab;
    public GameObject alphabetTilePrefab;
    
    //to put the board in the middle of the screen
    private float _xTransition;
    private float _yTransition;
    public float tileSize = 1f;
    
    //to fit the board to screen
    private float _xScaleValue;
    private float _yScaleValue;

    //to have different map shapes
    [SerializeField] private int levelNumber;
    
    //private char[,] grid; // 2D array to hold the letters
    private bool[,] activeCells; // 2D array to track active grid cells

    //to create alphabetTiles
    private char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    private float[] letterPercentages = new float[26]
    {
        0.082f, 0.015f, 0.028f, 0.043f, 0.127f, 0.022f, 0.02f, 0.061f, 0.07f, 0.002f, 0.008f, 0.04f, 0.024f, 0.067f,
        0.075f, 0.019f, 0.001f, 0.057f, 0.063f, 0.091f, 0.028f, 0.01f, 0.024f, 0.002f, 0.02f, 0.001f
    };

    private List<GameObject> lettersList = new List<GameObject>();
    private float _duration = 1f;

    void Start()
    {
        //grid = new char[rows, columns];
        activeCells = new bool[rows, columns];

        ScaleBoardToScreen();
        GenerateGrid();
        CreateMapTiles();
        CreateAlphabetTiles();
    }

    void ScaleBoardToScreen()
    {
        _xScaleValue = (float)transform.localScale.x;
        _yScaleValue = (float)transform.localScale.y;
        
        if (columns * _xScaleValue < Camera.main.orthographicSize || rows *_yScaleValue < Camera.main.orthographicSize)
        {
            var screenXRatio = Camera.main.orthographicSize / columns;
            var screenYRatio = Camera.main.orthographicSize / rows;
            var xNewScale = transform.localScale;
            xNewScale.x = _xScaleValue * screenXRatio;
            xNewScale.y = _yScaleValue * screenYRatio;
            transform.localScale = xNewScale;
        }
        
        //Debug.Log(_xScaleValue +" "+ _yScaleValue);
    }

    void GenerateGrid()
    {
        // Initialize the grid with random letters
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                //grid[i, j] = (char)Random.Range('A', 'Z' + 1);
                activeCells[i, j] = true; // By default, all cells are active
                
            }
        }

        // Set activeCells to false based on desired shapes
        SetShape(levelNumber); // Call to set the shape for the level
    }

    void SetShape(int level)
    {
        // Example shapes based on level
        switch (level)
        {
            case 1: // Square shape (default)
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        activeCells[i, j] = true;
                    }
                }
                break;
            case 2: // Mountain shape
                SetMountainShape();
                break;
            case 3: // Heart shape
                SetHeartShape();
                
                break;
            default:
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        activeCells[i, j] = true;
                    }
                }
                break;
        }
    }

    void SetMountainShape()
    {
        // Example heart shape for a 7*7 grid
        int[,] mountainShape = {
            {1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1},
            {0, 1, 1, 1, 1, 1, 0},
            {0, 0, 1, 1, 1, 0, 0},
            {0, 0, 0, 1, 0, 0, 0}
        };

        // Update the activeCells array based on the heart shape
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (mountainShape[i, j] == 0)
                {
                    activeCells[i, j] = false; // Inactive cell
                }
            }
        }
    }
    void SetHeartShape()
    {
        int[,] heartShape = {
            {0, 0, 1, 0, 1, 0, 0},
            {0, 1, 1, 1, 1, 1, 0},
            {1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1},
            {0, 1, 1, 1, 1, 1, 0},
            {0, 0, 1, 1, 1, 0, 0},
            {0, 0, 0, 1, 0, 0, 0}
        };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (heartShape[i, j] == 0)
                {
                    activeCells[i, j] = false;
                }
            }
        }
    }

    void CreateMapTiles()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (activeCells[i, j])
                {
                    GameObject tile = Instantiate(mapTilePrefab,transform.GetChild(0));

                    // Position the tile based on its grid location
                    _xTransition = (tileSize / 2) * rows - 50;
                    _yTransition = (tileSize / 2) * columns;
                    tile.transform.localPosition =
                        new Vector3((j * tileSize) - _xTransition, (i * tileSize) - _yTransition, 0);
                }
            }
        }
    }

    public void CreateAlphabetTiles()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (activeCells[i, j])
                {
                    char letter = GetRandomLetter();
                    GameObject letterTile = Instantiate(alphabetTilePrefab, transform.GetChild(1));
                    letterTile.GetComponentInChildren<TMP_Text>().text = letter.ToString();

                    //set on Top of the Map
                    _xTransition = (tileSize / 2) * rows - 50;
                    _yTransition = (tileSize / 2) * columns;
                    float highestY = transform.position.y + _yTransition - 100;
                    letterTile.transform.localPosition = new Vector3(transform.position.x , highestY, 0);
                    
                    //Add lettersTile to list
                    lettersList.Add(letterTile);
                    
                    int lettersCount = lettersList.Count;
                    
                    //To move to their tiles
                    Vector3 targetPos = new Vector3(((j * tileSize) - _xTransition) / 68,
                        ((i * tileSize) - _yTransition) / 68, 0);
                    lettersList[lettersCount-1].transform.DOMove(targetPos, _duration).SetEase(Ease.OutCubic);
                    _duration += 0.1f;
                }
            }
        }
    }
    
    private char GetRandomLetter()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulative = 0f;

        for (int i = 0; i < letters.Length; i++)
        {
            cumulative += letterPercentages[i];
            if (randomValue <= cumulative)
            {
                return letters[i];
            }
        }
        return 'A'; // Fallback
    }
}
