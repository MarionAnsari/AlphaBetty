using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
   //to check if alphabets can sit in it or not
   private bool _isUsable;
   
   // a reference to alphabet tiles
   private GameObject _alphabetTile;

   public Node(bool isUsable, GameObject alphabetTile)
   {
     _isUsable = isUsable;
     _alphabetTile = alphabetTile;
   }
}
