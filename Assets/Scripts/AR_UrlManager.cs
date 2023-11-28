using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_UrlManager : MonoBehaviour
{
    public Vimeo.Player.VimeoPlayer VideoPlayer;
    public VideoScreenManager VideoScreenManager;

    UniWebView webView;
    public string url;

    public void LoadUrl()
    {
        //UniWebView TempWebView = gameObject.GetComponent<UniWebView>();
        //if (TempWebView != null)
        //{
        //    Destroy(TempWebView);
        //}
        webView = gameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        webView.Load(url);

        webView.OnShouldClose += (view) => {
            VideoPlayer.IsUniWebViewOpened = false;
            VideoScreenManager.IsUrlOpened = false;
            VideoScreenManager.PlayPauseImage.SetActive(true);
            webView = null;
            LoadUrl();
            return true;
        };
    }
    public void SetResolution()
    {
        UniWebView TempWebView = gameObject.GetComponent<UniWebView>();
        if (TempWebView != null)
        {
            Destroy(TempWebView);
        }
        webView = gameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        webView.Load(url);

        webView.OnShouldClose += (view) => {
            VideoPlayer.IsUniWebViewOpened = false;
            VideoScreenManager.IsUrlOpened = false;
            VideoScreenManager.PlayPauseImage.SetActive(true);
            webView = null;
            LoadUrl();
            return true;
        };
        Debug.Log("Reloaded");
    }
    public void OpenUrl()
    {
        VideoScreenManager.PlayPauseImage.SetActive(true);
        VideoScreenManager.IsUrlOpened = true;
        LoadingManager.Instance.Loading(true);
        webView.Show();
        webView.EmbeddedToolbar.Show();
        LoadingManager.Instance.Loading(false);
    }
}
