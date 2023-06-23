using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
