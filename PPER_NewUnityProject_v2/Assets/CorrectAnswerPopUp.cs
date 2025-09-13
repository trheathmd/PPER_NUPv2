using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameVanilla.Game.Popups;
using GameVanilla.Core;
using TMPro;

public class CorrectAnswerPopUp : Popup
{
    [SerializeField] TextMeshProUGUI correctAnswer;


    public void Initialize(string text)
    {
        correctAnswer.text = text;
    }
}
