using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "WordData", fileName = "WordsList")]
public class WordData : ScriptableObject
{
    public List<string> ThreeLetters = new List<string>
    {
        "ARM", "BAG", "BAR", "BAT", "BED", "BOX", "BOY", "BUG", "BUS", "CAN", "CAR", "CAT", "CUP", "DEN", "DOG", "DOT", "DRY",
        "END", "FAN", "FED", "FOG", "FOR", "FOX", "FUN", "GOD", "GOT", "GUM", "GYM", "HAT", "HIP", "HOT", "INK", "JAM", "JOB",
        "KEY", "KID", "LAB", "LAW", "LEG", "LID", "LIP", "LOT", "MAP", "MAY", "MEN", "MOW", "MUD", "MUG", "NAP", "NUT", "OLD",
        "PAN", "PAT", "PEN", "PIG", "PIN", "RAG", "RAM", "RAT", "RIB", "ROD", "RUG", "RUN", "SAD", "SEE", "SEW", "SHE", "SHY",
        "SIN", "SLY", "SON", "SUN", "TAG", "TIP", "TOE", "TOP", "TRY", "VAN", "WET", "WIN", "WOK", "YES", "YET"
    };
    public List<string> FourLetters = new List<string>
    {
        "AWAY", "BACK", "BASE", "BEEN", "BODY", "BOTH", "CALL", "CARE", "CITY", "COME",
        "DOES", "DOWN", "EACH", "EVEN", "FEEL", "FILM", "FIND", "FROM", "GIVE", "GOOD", 
        "HAND", "HARD", "HAVE", "HELP", "HEAR", "HERE", "HIGH", "HOLD", "INTO", "KEEP",
        "LAST", "LEAD", "LIKE", "LINE", "LIST", "LONG", "LOOK", "MADE", "MAKE", "MANY", 
        "MORE", "MOST", "MOVE", "MUCH", "NEXT", "ONCE", "ONLY", "OVER", "PAGE", "PART", 
        "PLAY", "READ", "SAME", "SEAR", "SEEN", "SOME", "STAR", "STOP", "SUCH", "TELL",
        "THAN", "THAT", "THEM", "THEY", "THIS", "TIME", "TOES", "TURN", "VERY", "WANT",
        "WELL", "WERE", "WHAT", "WHEN", "WILL", "WITH", "WORK", "YOUR"
    };

}
