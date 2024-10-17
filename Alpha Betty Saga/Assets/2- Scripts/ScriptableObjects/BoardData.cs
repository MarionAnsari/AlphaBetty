using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    #region SearchingWordClass

    [System.Serializable]
    public class SearchingWord
    {
        public string word;
    }

    #endregion
    
    #region BoardRowClass

    [System.Serializable]
    public class BoardRow
    {
        public int size;
        public string[] row;

        public BoardRow()
        {
        }
        public BoardRow(int Size)
        {
            CreateRow(Size);
        }

        public void CreateRow(int Size)
        {
            size = Size;
            row = new string[size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < size; i++)
            {
                row[i] = "";
            }
        }

    }

    #endregion
    
    public float levelTime;
    public int columns;
    public int rows;

    public BoardRow[] board;
    
    /// <summary>
    /// suppose I need this list for my word searching system
    /// </summary>
    public List<SearchingWord> searchingWords = new List<SearchingWord>();

    public void ClearEmptyString()
    {
        for (int i = 0; i < columns; i++)
        {
           board[i].ClearRow(); 
        }
    }

    public void CreateNewBoard()
    {
        board = new BoardRow[columns];
        for (int i = 0; i < columns; i++)
        {
            board[i] = new BoardRow(rows);
        }
    }

}


