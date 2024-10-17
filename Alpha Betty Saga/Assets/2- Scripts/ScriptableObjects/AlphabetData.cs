using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class AlphabetData : ScriptableObject
{
    [System.Serializable]
    public class LetterData
    {
        public string letterName;
        public Sprite letterImage;
    }
    
    public List<LetterData> alphabetWhite = new List<LetterData>();
    public List<LetterData> alphabetBlue = new List<LetterData>();
    public List<LetterData> alphabetGreen = new List<LetterData>();
    public List<LetterData> alphabetRed = new List<LetterData>();
}
