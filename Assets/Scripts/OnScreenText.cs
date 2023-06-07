using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OnScreenText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private CanvasGroup myUIgroup;

    private bool fadeIn = false;
    private bool fadeOut = false;

    void Awake()
    {
        myUIgroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (fadeIn)
        {
            if (myUIgroup.alpha < 1)
            {
                myUIgroup.alpha += Time.deltaTime;
                if (myUIgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (myUIgroup.alpha >= 0)
            {
                myUIgroup.alpha -= Time.deltaTime;
                if (myUIgroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void setText(String input)
    {
        text.text = input;
    }

    public void clearText()
    {
        text.text = "";
    }

    public void StartFadeIn()
    {
        fadeIn = true;
    }

    public void StartFadeOut()
    {
        fadeOut = true;
    }

    public void Show()
    {
        myUIgroup.alpha = 1;
    }
}
