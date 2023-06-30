using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    public void Open_URL(string Uri)
    {
        Application.OpenURL(Uri);
    }
}
