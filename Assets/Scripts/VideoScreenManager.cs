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
        //if (ResolutionChanged)
        //{
        //    Debug.Log("ResolutionChanged and target found");

        //    //canvas.transform.position = _worldPosition;
        //    //canvas.transform.rotation = _worldRotation;
        //    //canvas.transform.localScale = _worldScale;

        //    //// Change the render mode back to World Space.
        //    //canvas.renderMode = RenderMode.WorldSpace;
        //    //canvas.transform.localScale = _worldScale;
        //    //canvas.transform.SetParent(AR_Camera.transform);


        //    //canvas.transform.position = _worldPosition;
        //    //canvas.transform.rotation = _worldRotation;
        //    //canvas.transform.localScale = _worldScale;


        //    //Screen.orientation = ScreenOrientation.Portrait;
        //}

    }
    public void OnTarget_Lost() // OnTargetLost
    {
        Debug.Log("target lost");
        //if (VideoPlayer.IsVideoPlayed)
        //{
        //    Debug.Log("target lost");
        //    //_worldPosition = canvas.transform.position;
        //    //_worldRotation = canvas.transform.rotation;
        //    //_worldScale = canvas.transform.localScale;

        //    //// Change the render mode to ScreenSpaceOverlay.
        //    //canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //    //// Store the current position of the canvas in world space.


        //    //// Disable the canvas's world position.
        //    //canvas.transform.position = Vector3.zero;

        //    ////Screen.orientation = ScreenOrientation.LandscapeLeft;
        //    //ResolutionChanged = true;
        //}

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
