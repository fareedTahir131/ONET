using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageMananger : MonoBehaviour
{
    

    public Screen[] UI_Screens;

    private string Language = "";
    void Start()
    {
        //TranslateText(0);
        TranslateText();
    }
    public void TranslateText()
    {

        if (PlayerPrefs.GetString("SelectedLang") == "")
        {
            //Language = LanguageDropdown.options[LanguageDropdown.value].text.ToString();
            PlayerPrefs.SetString("SelectedLang", "English");
            Language = "English";
            Debug.Log("Language" + Language);
        }
        else
        {
            Language = PlayerPrefs.GetString("SelectedLang");
        }
        if (Language == "English")
        {
            Debug.Log("Selected Language is "+ Language);
            TranslateTextToEnglish();
        }
        else if (Language == "Italian")
        {
            Debug.Log("Selected Language is " + Language);
            TranslateTextToItalian();
        }
        else if (Language == "German")
        {
            Debug.Log("Selected Language is " + Language);
            TranslateTextToGerman();
        }
        else if (Language == "French")
        {
            Debug.Log("Selected Language is " + Language);
            TranslateTextToFrench();
        }
        else if (Language == "Spanish")
        {
            Debug.Log("Selected Language is " + Language);
            TranslateTextToSpanish();
        }
        else if (Language == "Romanian")
        {
            Debug.Log("Selected Language is " + Language);
            TranslateTextToRomanian();
        }
        else
        {
            Debug.Log("No Language Found");
            TranslateTextToEnglish();
        }
    }
    public void TranslateTextToEnglish()
    {
        for (int i = 0; i < UI_Screens.Length; i++)
        {
            for (int j = 0; j < UI_Screens[i].UI_Texts.Length; j++)
            {
                UI_Screens[i].UI_Texts[j].text = UI_Screens[i].language.English[j];
            }
        }
    }
    public void TranslateTextToItalian()
    {
        for (int i = 0; i < UI_Screens.Length; i++)
        {
            for (int j = 0; j < UI_Screens[i].UI_Texts.Length; j++)
            {
                UI_Screens[i].UI_Texts[j].text = UI_Screens[i].language.Italian[j];
            }
        }
    }
    public void TranslateTextToGerman()
    {
        for (int i = 0; i < UI_Screens.Length; i++)
        {
            for (int j = 0; j < UI_Screens[i].UI_Texts.Length; j++)
            {
                UI_Screens[i].UI_Texts[j].text = UI_Screens[i].language.German[j];
            }
        }
    }
    public void TranslateTextToFrench()
    {
        for (int i = 0; i < UI_Screens.Length; i++)
        {
            for (int j = 0; j < UI_Screens[i].UI_Texts.Length; j++)
            {
                UI_Screens[i].UI_Texts[j].text = UI_Screens[i].language.French[j];
            }
        }
    }
    public void TranslateTextToSpanish()
    {
        for (int i = 0; i < UI_Screens.Length; i++)
        {
            for (int j = 0; j < UI_Screens[i].UI_Texts.Length; j++)
            {
                UI_Screens[i].UI_Texts[j].text = UI_Screens[i].language.Spanish[j];
            }
        }
    }
    public void TranslateTextToRomanian()
    {
        for (int i = 0; i < UI_Screens.Length; i++)
        {
            for (int j = 0; j < UI_Screens[i].UI_Texts.Length; j++)
            {
                UI_Screens[i].UI_Texts[j].text = UI_Screens[i].language.Romanian[j];
            }
        }
    }
}
