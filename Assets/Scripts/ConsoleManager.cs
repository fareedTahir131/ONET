using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    public static ConsoleManager instance;
    public TranslationClasses.Languages languages;
    private int LanguageIndex = -1;
    private string Language = "";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private Text messageText;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        messageText = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        message = TranslateMsg(message);
        messageText.text = message;
        animator.SetTrigger("FadeIn");
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("FadeOut");
    }
    private string TranslateMsg(string Msg)
    {
        for (int i = 0; i < languages.English.Length; i++)
        {
            if (Msg== languages.English[i])
            {
                LanguageIndex = i;
                break;
            }
        }
        if (LanguageIndex>-1)
        {
            return Translate(LanguageIndex);
        }
        return Msg;
    }
    private string Translate(int index)
    {
        Language = PlayerPrefs.GetString("SelectedLang");
        if (Language == "English")
        {
            return languages.English[index];
        }
        else if (Language == "Italian")
        {
            return languages.Italian[index];
        }
        else if (Language == "German")
        {
            return languages.German[index];
        }
        else if (Language == "French")
        {
            return languages.French[index];
        }
        else if (Language == "Spanish")
        {
            return languages.Spanish[index];
        }
        else if (Language == "Romanian")
        {
            return languages.Romanian[index];
        }
        else
        {
            return languages.English[index];
        }
    }
}
