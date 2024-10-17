using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public int SquareIndex { get; set; }

    private AlphabetData.LetterData _normalLetterData;
    private AlphabetData.LetterData _draggedLetterData;
    private AlphabetData.LetterData _correctLetterData;
    private AlphabetData.LetterData _wrongLetterData;

    private SpriteRenderer _displayedImage;
    void Start()
    {
        _displayedImage = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(AlphabetData.LetterData normalLetterData, AlphabetData.LetterData draggedLetterData,
        AlphabetData.LetterData correctLetterData, AlphabetData.LetterData wrongLetterData)
    {
        _normalLetterData = normalLetterData;
        _draggedLetterData = draggedLetterData;
        _correctLetterData = correctLetterData;
        _wrongLetterData = wrongLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.letterImage;
    }
}
