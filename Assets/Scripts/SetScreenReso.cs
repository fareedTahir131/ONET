using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vimeo.Player;
using UnityEngine.UI;

public class SetScreenReso : MonoBehaviour
{
    public VimeoPlayer vimeoPlayer;
    public  RawImage rawImage;

    private bool ResolutionSet = false;
    void Start()
    {
        ResolutionSet = false;
        //vimeoPlayer = GetComponent<VimeoPlayer>();
        vimeoPlayer.OnVideoStart += OnVideoStart;
    }

    void OnDisable()
    {
        vimeoPlayer.OnVideoStart -= OnVideoStart;
    }

    private void OnVideoStart()
    {
        if (!ResolutionSet)
        {
            RectTransform canvasRect = rawImage.GetComponent<RectTransform>();

            //canvasRect.localPosition = Vector3.zero;
            rawImage.rectTransform.sizeDelta = new Vector2(vimeoPlayer.GetWidth(), vimeoPlayer.GetHeight());
            //rawImage.sizeDelta = new Vector2(vimeoPlayer.GetHeight(), vimeoPlayer.GetWidth());
            //canvas.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            //canvas.transform.localScale = new Vector3((0.001f*1.280f), 0.001f*0.720f, 0.001f);

            
            

            ResolutionSet = true;
        }
    }
}
