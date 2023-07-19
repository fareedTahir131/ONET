using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public GameObject LoadingPanel;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            Instance = this;
        }
    }
    public void Loading(bool value)
    {
        LoadingPanel.SetActive(value);
    }
}
