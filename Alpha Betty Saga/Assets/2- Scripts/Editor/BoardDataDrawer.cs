using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Random = UnityEngine.Random;

[CustomEditor(typeof(BoardData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
    private BoardData _boardDataInstance => target as BoardData;
    /// <summary>
    /// //suppose that I should change this reorderable list for my word searching system
    /// </summary>
    private ReorderableList _dataList;

    private void OnEnable()
    {
        InitializeReorderableList(ref _dataList, "searchingWords", "Searching Words");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        //show column and row fields in inspector
        DrawColumnsAndRowsInputField();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        //show fill table button in inspector
        FilledTableWithButton();
        //show upper case button in inspector
        ConvertToUpperAlphabet();
        //show clear table button in inspector
        ClearTableData();
        GUILayout.EndHorizontal();
        
        //show table in inspector
        if (_boardDataInstance.board != null && _boardDataInstance.columns > 0 && _boardDataInstance.rows > 0)
        {
            DrawBoardtable();
        }
        EditorGUILayout.Space();
        
        //show searching word in inspector
        _dataList.DoLayoutList();
        
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_boardDataInstance);
        }
    }

    private void DrawColumnsAndRowsInputField()
    {
        var columnsInput = _boardDataInstance.columns;
        var rowsInput = _boardDataInstance.rows;

        _boardDataInstance.columns = EditorGUILayout.IntField("Columns", _boardDataInstance.columns);
        _boardDataInstance.rows = EditorGUILayout.IntField("Rows", _boardDataInstance.rows);

        if (_boardDataInstance.columns != columnsInput || _boardDataInstance.rows != rowsInput &&
            _boardDataInstance.columns > 0 && _boardDataInstance.rows > 0)
        {
            _boardDataInstance.CreateNewBoard();
        }
    }

    private void DrawBoardtable()
    {
        var tableStyle = new GUIStyle("Box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;
        
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 35;
        
        var columnStyle = new GUIStyle();
        columnStyle.fixedWidth = 50;
        
        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.fixedWidth = 40;
        rowStyle.alignment = TextAnchor.MiddleCenter;
        
        var textStyle = new GUIStyle();
        textStyle.normal.background = Texture2D.grayTexture;
        textStyle.normal.textColor = Color.cyan;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal(tableStyle);
        for (int x = 0; x < _boardDataInstance.columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
            for (int y = 0; y < _boardDataInstance.rows; y++)
            {
                if (x >= 0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var character = (string)EditorGUILayout.TextArea(_boardDataInstance.board[x].row[y], textStyle);
                    if (_boardDataInstance.board[x].row[y].Length > 1)
                    {
                        character = _boardDataInstance.board[x].row[y].Substring(0, 1);
                    }
                    _boardDataInstance.board[x].row[y] = character;
                    EditorGUILayout.EndHorizontal();
                } 
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void InitializeReorderableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true,
            true);
        //lambda
        list.drawHeaderCallback = (Rect rect) => {EditorGUI.LabelField(rect, listLabel);};

        //lambda
        var searchingWordList = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = searchingWordList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("word"), GUIContent.none);
        };
    }

    private void ConvertToUpperAlphabet()
    {
        if (GUILayout.Button("To Upper Case"))
        {
            for (int i = 0; i < _boardDataInstance.columns; i++)
            {
                for (int j = 0; j < _boardDataInstance.rows; j++)
                {
                    var lowerCaseCounter = Regex.Matches(_boardDataInstance.board[i].row[j], @"[a-z]").Count;
                    if (lowerCaseCounter > 0)
                    {
                        _boardDataInstance.board[i].row[j] = _boardDataInstance.board[i].row[j].ToUpper();
                    }
                }
            }

            foreach (var searchingWord in _boardDataInstance.searchingWords)
            {
                var lowerCaseCounter = Regex.Matches(searchingWord.word, @"[a-z]").Count;
                if (lowerCaseCounter > 0)
                {
                    searchingWord.word = searchingWord.word.ToUpper();
                }
            }
        }
    }

    private void ClearTableData()
    {
        if (GUILayout.Button("Clear Table"))
        {
            for (int i = 0; i < _boardDataInstance.columns; i++)
            {
                for (int j = 0; j < _boardDataInstance.rows; j++)
                {
                    _boardDataInstance.board[i].row[j] = " ";
                }
            }
        }
    }

    /// <summary>
    /// suppose I should change this method for my word searching system
    /// </summary>
    private void FilledTableWithButton()
    {
        if (GUILayout.Button("Fill Table"))
        {
            for (int i = 0; i < _boardDataInstance.columns; i++)
            {
                for (int j = 0; j < _boardDataInstance.rows; j++)
                {
                    var filledTilesCounter = Regex.Matches(_boardDataInstance.board[i].row[j], @"[a-zA-Z]").Count;
                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    
                    //////////////????????????????? here I should add the percentage of letters
                    int letterIndex = Random.Range(0, letters.Length);

                    if (filledTilesCounter == 0)
                    {
                        _boardDataInstance.board[i].row[j] = letters[letterIndex].ToString();
                    }
                }
            }
        }
    }
}
