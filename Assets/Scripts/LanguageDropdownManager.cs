using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdownManager : MonoBehaviour
{
    public Dropdown LanguageDropdown;

    void Start()
    {
        GetSelectedLanguage();
    }
    public void OnLanguageChange()
    {
        PlayerPrefs.SetString("SelectedLang", LanguageDropdown.options[LanguageDropdown.value].text.ToString());
    }
    public void GetSelectedLanguage()
    {
        //LanguageDropdown.value = LanguageDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetString("SelectedLang"));
        if (PlayerPrefs.GetString("SelectedLang") != "")
        {
            LanguageDropdown.value = LanguageDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetString("SelectedLang"));
            //PlayerPrefs.SetString("SelectedLang", LanguageDropdown.options[LanguageDropdown.value].text.ToString());
        }
        //else
        //{
        //    LanguageDropdown.value = LanguageDropdown.options.FindIndex(option => option.text == PlayerPrefs.GetString("SelectedLang"));
        //}
    }
}
