using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    // define size of the board
    [SerializeField]private int width;
    [SerializeField]private int height;
    
    //define somespace for the board
    private float _spaceX;
    private float _spaceY;

    //ref to alphabets
    [SerializeField] private GameObject[] backgroundTilesPrefab;
    
    //ref to collection of BackgroundTiles+ their GameObject
    private Node[,] _backgroundBoard;
    [SerializeField] private GameObject backgroundBoardGO;

    #region Singleton
    
    public static Board Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    private void Start()
    {
        InitializeBoard();
    }

    void InitializeBoard()
    {
        _backgroundBoard = new Node[width, height];
        _spaceX = (float)(width - 1) / 2;
        _spaceY = (float)(height - 1) / 2;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 spawnPos = new Vector2(x - _spaceX, y - _spaceY);
                
                //define an int for map tiles number
                //define an int for empty tiles in map
                // make a random generation in which these ints are percentage of each prefab )empty prefab or tile prefab)
                int randomPrefab = Random.Range(0, 2);
                GameObject backgroundMap = Instantiate(backgroundTilesPrefab[randomPrefab], spawnPos, Quaternion.identity);
                backgroundMap.transform.parent = this.transform;
            }
        }
    }
}
