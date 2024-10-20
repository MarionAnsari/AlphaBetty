using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DictionaryCheck : MonoBehaviour
{
    public static DictionaryCheck instance;
    private Dictionary<string, string> myDictionary;
    private string wordToCheck;
    public bool isCorrect;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        TextAsset myTextAsset = Resources.Load<TextAsset>("dictionary");
        myDictionary = myTextAsset.text.Split('\n').ToDictionary(w => w);
    }

    public bool CheckWord(string word)
    {
        return myDictionary.ContainsKey(word);
    }

    void Update()
    {
        if (CheckWord(wordToCheck))
        {
            isCorrect = true;
            Debug.Log(wordToCheck + " is a valid word");
        }
        else
            Debug.Log(wordToCheck + " is NOT a valid word");
    }
}