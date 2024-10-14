using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region LetterDataClass
[System.Serializable]
public class LetterData
{
    public string letterName;
    public Sprite letterImage;
}
#endregion

[System.Serializable]
[CreateAssetMenu]
public class AlphabetData : ScriptableObject
{
    public List<LetterData> alphabetWhite = new List<LetterData>();
    public List<LetterData> alphabetBlue = new List<LetterData>();
    public List<LetterData> alphabetGreen = new List<LetterData>();
    public List<LetterData> alphabetRed = new List<LetterData>();
}
