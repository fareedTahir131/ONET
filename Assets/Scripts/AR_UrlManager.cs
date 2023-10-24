using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_UrlManager : MonoBehaviour
{
    public Vimeo.Player.VimeoPlayer VideoPlayer;
    public VideoScreenManager VideoScreenManager;

    UniWebView webView;

    public void OpenUrl(string url)
    {
        VideoScreenManager.PlayPauseImage.SetActive(true);
        VideoScreenManager.IsUrlOpened = true;
        webView = gameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        LoadingManager.Instance.Loading(true);
        webView.Load(url);

        webView.OnShouldClose += (view) => {
            VideoPlayer.IsUniWebViewOpened = false;
            VideoScreenManager.IsUrlOpened = false;
            VideoScreenManager.PlayPauseImage.SetActive(true);
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
