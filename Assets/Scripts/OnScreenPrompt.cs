using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OnScreenPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void setText(String input)
    {
        text.text = input;
    }

    public void clearText()
    {
        text.text = "";
    }
}
