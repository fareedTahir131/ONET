using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WebViewUrlManager : MonoBehaviour
{
    public string url;
    UniWebView webView;

    private void Start()
    {
        LoadWebView();
    }

    public void OpenUrl()
    {
        LoadingManager.Instance.Loading(true);
        webView.Show();
        webView.EmbeddedToolbar.Show();
        LoadingManager.Instance.Loading(false);

    }
    void LoadWebView()
    {
        webView = gameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        //LoadingManager.Instance.Loading(true);
        webView.Load(url);

        webView.OnShouldClose += (view) => {
            webView = null;
            LoadWebView();
            return true;
        };
    }
}
