using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_UrlManager : MonoBehaviour
{
    public Vimeo.Player.VimeoPlayer VideoPlayer;

    UniWebView webView;

    public void OpenUrl(string url)
    {
        webView = gameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        LoadingManager.Instance.Loading(true);
        webView.Load(url);

        webView.OnShouldClose += (view) => {
            VideoPlayer.IsUniWebViewOpened = false;
            webView = null;
            return true;
        };

        webView.OnPageFinished += (view, statusCode, url) => {
            Invoke(nameof(ShowWebView), 1);
        };
    }
    void ShowWebView()
    {
        webView.Show();
        webView.EmbeddedToolbar.Show();
        LoadingManager.Instance.Loading(false);
    }
}
