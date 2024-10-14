using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetTile : MonoBehaviour
{
    public Alphabets alphabets;
    //for getting x and y positions
    private int _xIndex;
    private int _yIndex;

    private bool _isMatched;
    private Vector2 _currentPos;
    private Vector2 _targetPos;

    bool _isMoving;

    public AlphabetTile(int x, int y)
    {
        _xIndex = x;
        _yIndex = y;
    }
    
    public void SetIndicies(int x, int y)
    {
        _xIndex = x;
        _yIndex = y;
    }

}

public enum Alphabets
{
    A, B, C, D, E, F, G, H, I, J, K, L, M, 
    N, O, P, Q, R, S, T, U, V, W, X, Y, Z
}
