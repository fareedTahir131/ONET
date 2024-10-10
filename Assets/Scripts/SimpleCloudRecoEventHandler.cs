using System.Collections;
using TriLibCore.Samples;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";
    private string WebUrl = "";
    public ImageTargetBehaviour ImageTargetTemplate;
    //public LoadModelFromURLSample ModelLoader;

    //public ObjectDownloader ModelDownloader;
    public LoadModelFromURLSample loadModelFromURLSample;
    public VideoScreenManager ScreenManager;

    public GameObject AR_Loading;
    public GameObject VideoObject;
    public GameObject WebsiteButton;
    public string MetData;

    public Vimeo.Player.VimeoPlayer VideoPlayer;
    public AR_UrlManager UrlManager;

    private MetaDataRoot ImageMetaData;
    private API_Root API_Data;
    private bool ModelDataFound = false;
    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);

        ModelDataFound = false;
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
    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }
    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }
    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }
    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            //var tracker = SimpleCloudRecoEventHandler.TrackerManager.Instance.GetTracker<ObjectTracker>();
            //tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }
    }
    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        //CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult =
        //    (CloudRecoBehaviour.CloudRecoSearchResult)cloudRecoSearchResult;
        // do something with the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;
        Debug.Log("mTargetMetadata " + mTargetMetadata);
        ImageMetaData = JsonUtility.FromJson<MetaDataRoot>(mTargetMetadata);
        Debug.Log("Image ID "+ ImageMetaData.id);
        API_Root apiData = MapMetaDataToAPI(ImageMetaData);
        PerformOperation(apiData); // Comment this line and enable GetData Coroutine to load model and video.
        //StartCoroutine(GetData(ImageMetaData.id));

        //MetaDataRoot ImageMetaData = JsonUtility.FromJson<MetaDataRoot>(mTargetMetadata);
        // stop the target finder (i.e. stop scanning the cloud)
        mCloudRecoBehaviour.enabled = false;
        // Build augmentation based on target 
        if (ImageTargetTemplate)
        {
            // enable the new result with the same ImageTargetBehaviour: 
            mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        }
    }
    public API_Root MapMetaDataToAPI(MetaDataRoot metaData)
    {
        API_Root apiData = new API_Root
        {
            success = true, // Assuming success is true by default
            id = metaData.id,
            target_name = metaData.target_name,
            video_id = metaData.video_id,
            video_url = metaData.video_url,
            image_link = metaData.image_link,
            model_image_link = metaData.model_image_link,
            texture_link = metaData.texture_link,
            website_url = metaData.website_url
        };

        return apiData;
    }
    public void OpenWebUrl()
    {
        if (!ModelDataFound)
        {
            //Application.OpenURL(WebUrl);
            VideoPlayer.IsUniWebViewOpened = true;
            VideoPlayer.Pause();
            UrlManager.OpenUrl();
            //UrlManager.OpenUrl(WebUrl);
        }
        else
        {
            VideoPlayer.IsUniWebViewOpened = true;
            UrlManager.OpenUrl();
        }
        
    }
    public void VideoPlayPuaseManager()
    {
        if (!ModelDataFound)
        {
            ScreenManager.PlayPauseImageManager();
        }
    }
    public IEnumerator GetData(int ID)
    {
        string requestName = "http://16.171.149.101/projects/show_project?project_id=" + ID;

        using (UnityWebRequest www = UnityWebRequest.Get(requestName))
        {
            //www.SetRequestHeader("Authorization", "Bearer " + Auth0Manager.AccessToken);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                //ConsoleManager.instance.ShowMessage("Network Error!");
                Debug.Log(www.error);
            }
            else if (www.isHttpError)
            {
                if (www.responseCode==422)
                {
                    Debug.Log("Invalid Image "+www.error);
                }
            }
            else
            {
                API_Root API_Data = JsonUtility.FromJson<API_Root>(www.downloadHandler.text);
                Debug.Log("responce data "+www.downloadHandler.text);
                if (!API_Data.success)
                {
                    Debug.Log("Target data error!");
                    //ConsoleManager.instance.ShowMessage("Target data error!");
                }
                else
                {
                    PerformOperation(API_Data);
                }
                LoadingManager.Instance.Loading(false);
            }
        }
    }
    private void PerformOperation(API_Root API_Data)
    {
        if (ImageMetaData.model_image_link == "" || ImageMetaData.model_image_link == null)
        {
            AR_Loading.SetActive(false);
            VideoObject.SetActive(true);
            VideoPlayer.SetVideoLinkAndPlay(mTargetMetadata);
        }
        else
        {
            Debug.Log("Model Code has been disabled temporarily.");
            /*AR_Loading.SetActive(true);
            VideoObject.SetActive(false);
            ModelDataFound = true;
            loadModelFromURLSample.ModelUrl = ImageMetaData.model_image_link;
            loadModelFromURLSample.LoadModel();
            //ModelDownloader.DownloadModel(API_Data.model_image_link, API_Data.texture_link);
            //ModelLoader.ModelUrl = API_Data.model_image_link;
            //ModelLoader.LoadModel();*/
        }
        try
        {
            //MetaDataRoot ImageMetaData = JsonUtility.FromJson<MetaDataRoot>(mTargetMetadata);
            if (ImageMetaData.website_url != "" || ImageMetaData.website_url != null)
            {
                WebUrl = ImageMetaData.website_url;
                Debug.Log("WebUrl "+ WebUrl);
                UrlManager.url = WebUrl;
                UrlManager.LoadUrl();
                WebsiteButton.SetActive(true);
            }
            else
            {
                WebsiteButton.SetActive(false);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("ex " + ex);
        }
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