using TMPro;
using UnityEngine;

public static class GameEvents
{
    public delegate void EnableSquareSelection();

    public static event EnableSquareSelection OnEnableSquareSelection;

    public static void EnableSquareSelectionMethod()
    {
        if (OnEnableSquareSelection != null)
        {
            OnEnableSquareSelection();
        }
    }
    
    //...........................................................
    public delegate void DisableSquareSelection();

    public static event DisableSquareSelection OnDisableSquareSelection;

    public static void DisableSquareSelectionMethod()
    {
        if (OnDisableSquareSelection != null)
        {
            OnDisableSquareSelection();
        }
    }
    
    //...........................................................
    public delegate void SelectSquare(Vector3 position);

    public static event SelectSquare OnSelectSquare;

    public static void SelectSquareMethod(Vector3 position)
    {
        if (OnSelectSquare != null)
        {
            OnSelectSquare(position);
        }
    }
    
    //...........................................................
    public delegate void CheckSquare(TMP_Text letterText, Vector3 squarePos, int squareIndex);

    public static event CheckSquare OnCheckSquare;

    public static void CheckSquareMethod(TMP_Text letterText, Vector3 squarePos, int squareIndex)
    {
        if (OnCheckSquare != null)
        {
            OnCheckSquare(letterText, squarePos, squareIndex);
        }
    }
    
    //...........................................................
    public delegate void ClearSelection();

    public static event ClearSelection OnClearSelection;

    public static void ClearSelectionMethod()
    {
        if (OnClearSelection != null)
        {
            OnClearSelection();
        }
    }
}
