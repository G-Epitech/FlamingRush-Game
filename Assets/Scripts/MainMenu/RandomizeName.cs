using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class RandomizeName : MonoBehaviour
{
    private string[] _firstNames;
    private string[] _secondNames;
    public TMP_InputField inputField;

    private void Start()
    {
        _firstNames = new string[] { "Cute", "Shiny", "Dancy", "Silly", "Evil", "Glowy", "Only", "Sweet", "Sunny", "Happy", "Funky", "Spicy"};
        _secondNames = new string[]
        {
            "Frog", "Bird", "Clown", "Zest", "Cat", "Dog",
            "Fish", "Wolf", "Crab", "Koala", "Lion", "Bear",
            "Ant", "Owl", "Panda", "Deer", "Fox"
        };
    }

    public void randomizeName()
    {
        Random rnd = new Random();
        Random rnd2 = new Random();

        inputField.text = _firstNames[rnd.Next(1, this._firstNames.Length)] +
            _secondNames[rnd2.Next(1, this._secondNames.Length)];
    }
}
