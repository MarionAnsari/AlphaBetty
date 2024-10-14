using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int rows = 10;
    [SerializeField] private int columns = 10;

    [SerializeField] private int levelNumber;
    public GameObject tilePrefab;
    public float tileSize = 1f; 

    private char[,] grid; // 2D array to hold the letters
    private bool[,] activeCells; // 2D array to track active grid cells

    

    void Start()
    {
        grid = new char[rows, columns];
        activeCells = new bool[rows, columns];

        GenerateGrid();
        CreateTiles();
    }

    void GenerateGrid()
    {
        // Initialize the grid with random letters
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = (char)Random.Range('A', 'Z' + 1);
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
        // Example heart shape for a 10x10 grid
        int[,] mountainShape = {
            {0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
            {0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 1, 1, 1, 1, 1, 1, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
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
        // Example heart shape for a 10x10 grid
        int[,] heartShape = {
            {0, 0, 1, 0, 0, 0, 0, 1, 0, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 1, 1, 1, 1, 1, 1, 0, 0},
            {0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        // Update the activeCells array based on the heart shape
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (heartShape[i, j] == 0)
                {
                    activeCells[i, j] = false; // Inactive cell
                }
            }
        }
    }

    void CreateTiles()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (activeCells[i, j]) // Only create tiles for active cells
                {
                    GameObject tile = Instantiate(tilePrefab,transform);
                    
                    // Position the tile based on its grid location
                    tile.transform.localPosition = new Vector3((j * tileSize) - 4.5f, (-i * tileSize) + 4f, 0);
                }
            }
        }
    }
}
