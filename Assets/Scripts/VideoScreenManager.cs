using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoScreenManager : MonoBehaviour
{
    public Vimeo.Player.VimeoPlayer VideoPlayer;
    public Canvas canvas;
    public GameObject AR_Camera;


    private Vector3 _worldPosition;
    private Quaternion _worldRotation;
    private Vector3 _worldScale;

    private bool ResolutionChanged = false;
    private void Start()
    {
        ResolutionChanged = false;
        //StartCoroutine(Wait());
    }
    public void ChangeScreen()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //VideoPlayer.Pause();
        //VideoPlayer.videoScreen = rawImageScreen;
        //VideoPlayer.LoadVideo();
        //VideoPlayer.Play();
    }
    public void OnTarget_Found() // OnTargetFound
    {
        Debug.Log("target found");
        if (ResolutionChanged)
        {

            //canvas.transform.position = new Vector3(0,0,0);
            //canvas.transform.Rotate(new Vector3(90, 0, 0));// = new Vector3(90, 0, 0);
            //canvas.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            //// Change the render mode back to World Space.
            canvas.renderMode = RenderMode.WorldSpace;
            //canvas.transform.localScale = _worldScale;
            //canvas.transform.SetParent(AR_Camera.transform);

            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            canvasRect.localPosition = Vector3.zero;
            canvasRect.sizeDelta = new Vector2(1080, 2160);
            canvas.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            canvas.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            //canvas.transform.position = new Vector3(0, 0, 0);
            //canvas.transform.Rotate(new Vector3(90, 0, 0));// = new Vector3(90, 0, 0);
            //canvas.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            //var screenToWorldPosition = Camera.main.ScreenToWorldPoint(canvas.transform.position);
            Screen.orientation = ScreenOrientation.Portrait;
        }

    }
    public void OnTarget_Lost() // OnTargetLost
    {
        if (VideoPlayer.IsVideoPlayed)
        {
            _worldPosition = canvas.transform.position;
            _worldRotation = canvas.transform.rotation;
            _worldScale = canvas.transform.localScale;
            canvas.enabled = true;
            // Change the render mode to ScreenSpaceOverlay.
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            //// Store the current position of the canvas in world space.


            //// Disable the canvas's world position.
            //canvas.transform.position = Vector3.zero;

            Screen.orientation = ScreenOrientation.LandscapeLeft;
            ResolutionChanged = true;
        }

    }
    
    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(8);
    //    //ChangeScreen();
    //    ChangeToScreenSpace();
    //    StartCoroutine(Wait2());
    //}
    //IEnumerator Wait2()
    //{
    //    yield return new WaitForSeconds(3);
    //    //ChangeScreen();
    //    //ChangeToScreenSpace();
    //    ChangeToWorldSpace();
    //}
}
