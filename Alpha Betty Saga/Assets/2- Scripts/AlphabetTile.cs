using System;
using TMPro;
using UnityEngine;

public class AlphabetTile : MonoBehaviour
{
    //for changing the sprite
    private SpriteRenderer _alphabetSpriteRenderer;
    [SerializeField] private Sprite[] alphabetSprites; // 0 for normal, 1 for selected, 2 for correct, 3 for wrong
    
    // for checking the letter
    private TMP_Text _letterText;
    
    
    private bool _selected;
    private bool _clicked;
    private bool _isCorrect;
    
    private int _index = -1;

    public void SetIndex(int index)
    {
        _index = index;
    }

    public int GetIndex()
    {
        return _index;
    }

    private void Start()
    {
        _selected = false;
        _clicked = false;
        _isCorrect = false;
        _alphabetSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        GameEvents.OnEnableSquareSelection += OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        GameEvents.OnSelectSquare += OnSelectSquare;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableSquareSelection -= OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        GameEvents.OnSelectSquare -= OnSelectSquare;
    }

    public void OnEnableSquareSelection()
    {
        _clicked = true;
        _selected = false;
    }

    public void OnDisableSquareSelection()
    {
        _clicked = false;
        _selected = false;
        if (_isCorrect == true)
        {
            _alphabetSpriteRenderer.sprite = alphabetSprites[2];
        }
        else
        {
            ////////////// I want just the selected ones to be red??????????????????
            ////////// check if the position is selected then do the line
            _alphabetSpriteRenderer.sprite = alphabetSprites[0];
        }
    }

    public void OnSelectSquare(Vector3 position)
    {
        if (this.gameObject.transform.position == position)
        {
            _alphabetSpriteRenderer.sprite = alphabetSprites[1];
        }
    }

    private void OnMouseDown()
    {
        OnEnableSquareSelection();
        GameEvents.EnableSquareSelectionMethod();
        CheckSquare();
        _alphabetSpriteRenderer.sprite = alphabetSprites[1];
    }

    private void OnMouseEnter()
    {
        CheckSquare();
    }

    private void OnMouseUp()
    {
        GameEvents.ClearSelectionMethod();
        GameEvents.DisableSquareSelectionMethod();
    }

    public void CheckSquare()
    {
        if (_selected == false && _clicked == true)
        {
            _selected = true;
            ///////////Im not sure i need this lettertext and index???????????????????????????????????
            GameEvents.CheckSquareMethod(_letterText, gameObject.transform.position, _index);
        }
    }
}
