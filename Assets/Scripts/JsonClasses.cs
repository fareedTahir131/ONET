using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class JsonClasses : MonoBehaviour
{
}
[Serializable]
public class Root
{
    public User user;
}
[Serializable]
public class User
{
    public string email;
    public string password;
    public string name;
}

/// <summary>
/// LoginResponce
/// </summary>
namespace LoginResponce
{
    [Serializable]
    public class Root
    {
        public Status status;
    }
    [Serializable]
    public class Data
    {
        public User user;
    }
    [Serializable]
    public class Status
    {
        public int code;
        public string message;
        public Data data;
    }
    [Serializable]
    public class User
    {
        public int id;
        public string email;
        public string name;
    }
}

//////////
//Class for language
namespace TranslationClasses
{
    [Serializable]
    public class UI_Screens
    {
        public Screen[] screens;
    }
    [Serializable]
    public class Screen
    {
        public Text[] UI_Texts;
        public Languages language;
    }
    [Serializable]
    public class Languages
    {
        public string[] English;
        public string[] Italian;
        public string[] German;
        public string[] French;
        public string[] Spanish;
        public string[] Romanian;
    }
}

[Serializable]
public class MetaDataRoot
{
    //public int id;
    //public string video_url;
    //public string Model_url;
    //public string website_url;
    public int id;
    public string target_name;
    public object uploaded_by;
    public int video_id;
    public string video_url;
    public string image_link;
    public string model_image_link;
    public string texture_link;
    public string website_url;
}
[Serializable]
public class API_Root
{
    public bool success;
    public int id;
    public string target_name;
    public int video_id;
    public string video_url;
    public string image_link;
    public string model_image_link;
    public string texture_link;
    public string website_url;
}