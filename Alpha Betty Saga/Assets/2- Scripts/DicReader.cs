using System;
using UnityEngine;

public class DicReader : MonoBehaviour
{
    public TextAsset textAssetData;
    public WordData wordRef;
    
    [System.Serializable]
    public class Word
    {
        public string name;
        public int letterCount;
    }
    [System.Serializable]
    public class WordList
    {
        public Word[] word;
    }

    public WordList myWordList = new WordList();

    private void Start()
    {
        ReadDic();
        foreach (var word in wordRef.ThreeLetters)
        {
            Debug.Log("word" + word);
        }
    }

    void ReadDic()
    {
        string[] data = textAssetData.text.Split(new string[] {",", "\n"}, StringSplitOptions.None);
        int tableSize = data.Length / 2 - 1;
        myWordList.word = new Word[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            myWordList.word[i] = new Word();
            myWordList.word[i].name = data[2 * (i + 1)];
            myWordList.word[i].letterCount = int.Parse(data[2 * (i + 1) + 1]);
        }
    }
    
    public void GetWordByName(string wordName)
    {
        
        // foreach (Word word in myWordList.word)
        // {
        //     if (word.name.Equals(wordName))
        //     {
        //         Debug.Log("word found");
        //     }
        // }
    }
}
