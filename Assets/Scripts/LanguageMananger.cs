using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageMananger : MonoBehaviour
{
    public Screen[] UI_Screens;
    void Start()
    {
        TranslateText(0);
    }

    public void TranslateText(int LanguageIndex)
    {
        for (int i = 0; i < UI_Screens[LanguageIndex].UI_Texts.Length; i++)
        {
            UI_Screens[LanguageIndex].UI_Texts[i].text = UI_Screens[LanguageIndex].language.Italian[i];
        }
    }
}
