using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordChecker : MonoBehaviour
{
    //reference to my scriptable object
    public WordData wordRef;
    private string _word = "";

    //to limit the raycast
    private int _assignedPoints = 0;
    private int _completeWords = 0;
    private int _maxRay = 5;
    private Ray _currentRay = new Ray();
    private Ray _rayUp, _rayDown, _rayLeft, _rayRight;
    private Ray _rayDiagonalUpLeft, _rayDiagonalUpRight, _rayDiagonalDownLeft, _rayDiagonalDownRight;
    private Vector3 _rayStartPos;
    private List<int> _correctSquareList = new List<int>();

    private void OnEnable()
    {
        GameEvents.OnCheckSquare += CheckSquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= CheckSquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
    }

    private void Start()
    {
        _assignedPoints = 0;
        _completeWords = 0;
    }

    private void Update()
    {
        if (_assignedPoints > 0 && Application.isEditor)
        {
             Debug.DrawRay(_rayUp.origin, _rayUp.direction * 6);
             Debug.DrawRay(_rayDown.origin, _rayDown.direction * 6);
             Debug.DrawRay(_rayLeft.origin, _rayLeft.direction * 6);
             Debug.DrawRay(_rayRight.origin, _rayRight.direction * 6);
             Debug.DrawRay(_rayDiagonalUpLeft.origin, _rayDiagonalUpLeft.direction * 6);
             Debug.DrawRay(_rayDiagonalUpRight.origin, _rayDiagonalUpRight.direction * 6);
             Debug.DrawRay(_rayDiagonalDownLeft.origin, _rayDiagonalDownLeft.direction * 6);
             Debug.DrawRay(_rayDiagonalDownRight.origin, _rayDiagonalDownRight.direction * 6);
        }
    }

    private void CheckSquareSelected(string letterText, Vector3 squarePos, int squareIndex)
    {
        if (_assignedPoints <= _maxRay)
        {
            if (_assignedPoints == 0)
            {
                _rayStartPos = squarePos;
                _correctSquareList.Add(squareIndex);
                _word += letterText;

                _rayUp = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(0, 1));
                _rayDown = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(0, -1));
                _rayLeft = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(-1, 0));
                _rayRight = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(1, 0));
                _rayDiagonalUpLeft = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(-1, 1));
                _rayDiagonalUpRight = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(1, 1));
                _rayDiagonalDownLeft = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(-1, -1));
                _rayDiagonalDownRight = new Ray(new Vector2(squarePos.x, squarePos.y), new Vector2(1, -1));
            }
            else if(_assignedPoints > 0)
            {
                _correctSquareList.Add(squareIndex);
                _currentRay = SelectRay(_rayStartPos, squarePos);
                GameEvents.SelectSquareMethod(squarePos);
                _word += letterText;
                if (_assignedPoints == 2)
                {
                    Debug.Log("word is three letter");
                    CheckWordForThree();
                }
                if (_assignedPoints == 3)
                {
                    Debug.Log("word is four letter");
                    CheckWordForFour();
                }
            }
        }
        _assignedPoints++;
    }

    private void CheckWordForThree()
    {
        //////////////// here i need my main algorithm for searching the word???????????????????????????
        if (wordRef == null || wordRef.ThreeLetters == null)
        {
            Debug.LogError("Word reference is not set or the word list is empty.");
            return;
        }
        if (wordRef.ThreeLetters.Contains(_word))
        {
            Debug.Log("Word found:"  + _word);
            GameEvents.CorrectWordMethod(_word, _correctSquareList);
            _completeWords++;
            _word = string.Empty;
            _correctSquareList.Clear();
            
        }
        else
        {
            Debug.Log("Word  not found:"  + _word);
        }
         

    }
    private void CheckWordForFour()
    {
        //////////////// here i need my main algorithm for searching the word???????????????????????????
        if (wordRef == null || wordRef.FourLetters == null)
        {
            Debug.LogError("Word reference is not set or the word list is empty.");
            return;
        }
        if (wordRef.FourLetters.Contains(_word))
        {
            Debug.Log("Word found:"  + _word);
            GameEvents.CorrectWordMethod(_word, _correctSquareList);
            _completeWords++;
            _word = string.Empty;
            _correctSquareList.Clear();
            
        }
        else
        {
            Debug.Log("Word  not found:"  + _word);
        }
         

    }

    private Ray SelectRay(Vector2 firstPos, Vector2 secondPos)
    {
        Vector2 direction = (secondPos - firstPos).normalized;
        float tolerance = 0.01f;
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - (1)) < tolerance)
        {
            return _rayUp;
        }
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - (-1)) < tolerance)
        {
            return _rayDown;
        }
        if (Mathf.Abs(direction.x - (-1)) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return _rayLeft;
        }
        if (Mathf.Abs(direction.x - (1)) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return _rayRight;
        }
        if (direction.x < 0 && direction.y > 0)
        {
            return _rayDiagonalUpLeft;
        }
        if (direction.x > 0 && direction.y > 0)
        {
            return _rayDiagonalUpRight;
        }
        if (direction.x < 0 && direction.y < 0)
        {
            return _rayDiagonalDownLeft;
        }
        if (direction.x > 0 && direction.y < 0)
        {
            return _rayDiagonalDownRight;
        }
        
        return _rayDown;
    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }
}
