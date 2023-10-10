using TriLibCore.Samples;
using UnityEngine;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";
    private string WebUrl = "";
    public ImageTargetBehaviour ImageTargetTemplate;
    public LoadModelFromURLSample ModelLoader;

    public GameObject VideoObject;
    public GameObject WebsiteButton;
    public string MetData;

    public Vimeo.Player.VimeoPlayer VideoPlayer;
    public AR_UrlManager UrlManager;

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized");
    }
    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }
    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }
    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }
    }
    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        TargetFinder.CloudRecoSearchResult cloudRecoSearchResult =
            (TargetFinder.CloudRecoSearchResult)targetSearchResult;
        // do something with the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;
        Debug.Log("mTargetMetadata " + mTargetMetadata);
        MetaDataRoot ImageMetaData = JsonUtility.FromJson<MetaDataRoot>(mTargetMetadata);
        if (ImageMetaData.model_image_link=="" || ImageMetaData.model_image_link == null)
        {
            VideoObject.SetActive(true);
            VideoPlayer.SetVideoLinkAndPlay(mTargetMetadata);
        }
        else
        {
            VideoObject.SetActive(false);
            ModelLoader.LoadModel();
        }
        try
        {
            //MetaDataRoot ImageMetaData = JsonUtility.FromJson<MetaDataRoot>(mTargetMetadata);
            if (ImageMetaData.website_url != "" || ImageMetaData.website_url != null)
            {
                WebUrl = ImageMetaData.website_url;
                WebsiteButton.SetActive(true);
            }
            else
            {
                WebsiteButton.SetActive(false);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("ex "+ ex);
        }
        //MetaDataRoot ImageMetaData = JsonUtility.FromJson<MetaDataRoot>(mTargetMetadata);
        // stop the target finder (i.e. stop scanning the cloud)
        mCloudRecoBehaviour.CloudRecoEnabled = false;
        // Build augmentation based on target 
        if (ImageTargetTemplate)
        {
            // enable the new result with the same ImageTargetBehaviour: 
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate.gameObject);
        }
    }
    public void OpenWebUrl()
    {
        //Application.OpenURL(WebUrl);
        VideoPlayer.IsUniWebViewOpened = true;
        VideoPlayer.Pause();
        UrlManager.OpenUrl(WebUrl);
    }
    //void OnGUI()
    //{
    //    // Display current 'scanning' status
    //    GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
    //    // Display metadata of latest detected cloud-target
    //    GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);
    //    // If not scanning, show button
    //    // so that user can restart cloud scanning
    //    if (!mIsScanning)
    //    {
    //        if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
    //        {
    //            // Restart TargetFinder
    //            mCloudRecoBehaviour.CloudRecoEnabled = true;
    //        }
    //    }
    //}
}