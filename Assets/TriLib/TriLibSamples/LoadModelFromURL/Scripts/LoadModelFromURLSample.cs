using UnityEngine;
using UnityEngine.Networking;

namespace TriLibCore.Samples
{
    /// <summary>
    /// Represents a sample that loads a compressed (Zipped) Model.
    /// </summary>
    public class LoadModelFromURLSample : MonoBehaviour
    {
        public GameObject referenceModel;
        public GameObject ModelParent;
        public GameObject AR_Loading;
        public string ModelUrl;
        //public string ModelExtension;

        private bool IsModelTargetFound = false;
        private bool ModelLoaded = false;
        

        private UnityWebRequest webRequest;
        /// <summary>
        /// Creates the AssetLoaderOptions instance, configures the Web Request, and downloads the Model.
        /// </summary>
        /// <remarks>
        /// You can create the AssetLoaderOptions by right clicking on the Assets Explorer and selecting "TriLib->Create->AssetLoaderOptions->Pre-Built AssetLoaderOptions".
        /// </remarks>
        private void Start()
        {
            
            //var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            //var webRequest = AssetDownloader.CreateWebRequest(ModelUrl);
            ////var webRequest = AssetDownloader.CreateWebRequest("https://filebin.net/8skatrcwgypmky6s/craneo.OBJ");
            ////var webRequest = AssetDownloader.CreateWebRequest("https://filebin.net/95zui2czez94eifo/____.glb");
            ////var webRequest = AssetDownloader.CreateWebRequest("https://ricardoreis.net/trilib/demos/sample/TriLibSampleModel.zip");
            //AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions,null, ModelExtension, false);
            ////AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions,null, "obj", false);
            //LoadModel();
        }
        public void LoadModel()
        {
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            webRequest = AssetDownloader.CreateWebRequest(ModelUrl);
            //var webRequest = AssetDownloader.CreateWebRequest("https://filebin.net/8skatrcwgypmky6s/craneo.OBJ");
            //var webRequest = AssetDownloader.CreateWebRequest("https://filebin.net/95zui2czez94eifo/____.glb");
            //var webRequest = AssetDownloader.CreateWebRequest("https://ricardoreis.net/trilib/demos/sample/TriLibSampleModel.zip");
            AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions, null, null,true);
            //AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions,null, "obj", false);
            //webRequest.
            IsModelTargetFound = true;
            ModelLoaded = false;
        }
        /// <summary>
        /// Called when any error occurs.
        /// </summary>
        /// <param name="obj">The contextualized error, containing the original exception and the context passed to the method where the error was thrown.</param>
        private void OnError(IContextualizedError obj)
        {
            Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
        }

        /// <summary>
        /// Called when the Model loading progress changes.
        /// </summary>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        /// <param name="progress">The loading progress.</param>
        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            Debug.Log($"Loading Model. Progress: {progress:P}");
        }

        /// <summary>
        /// Called when the Model (including Textures and Materials) has been fully loaded.
        /// </summary>
        /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Materials loaded. Model fully loaded.");
            SetModel();
        }
        private void SetModel()
        {
            GameObject myObject = GameObject.Find("1");
            myObject.transform.parent = ModelParent.transform;

            // Set the position and scale of the object
            myObject.transform.localPosition = new Vector3(0f, 0f, 0f); // Set the local position
            myObject.transform.localScale = new Vector3(1f, 1f, 1f);

            SizeManager(myObject.transform.GetChild(0).gameObject);
            myObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            AR_Loading.SetActive(false);


            ModelLoaded = true;
        }
        public void ModelTargetFound()
        {
            if (IsModelTargetFound)
            {
                if (!ModelLoaded)
                {
                    webRequest.Abort();
                    LoadModel();
                }
            }
            
        }
        public void ModelTargetLost()
        {
            if (IsModelTargetFound)
            {
                if (!ModelLoaded)
                {
                    webRequest.Abort();
                    //StopAllCoroutines();
                }
            }
        }
        /// <summary>
        /// Called when the Model Meshes and hierarchy are loaded.
        /// </summary>
        /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Model loaded. Loading materials.");
        }

        void SizeManager(GameObject Model)
        {
            // Get the size of the reference model
            Vector3 referenceSize = GetObjectSize(referenceModel);

            // Get the size of the current object
            Vector3 objSize = GetObjectSize(Model);

            // Calculate the scale factors for each dimension
            float xScaleFactor = referenceSize.x / objSize.x;
            float yScaleFactor = referenceSize.y / objSize.y;
            float zScaleFactor = referenceSize.z / objSize.z;

            // Apply the scale factors uniformly to x, y, and z dimensions
            Model.transform.localScale = new Vector3(
                Model.transform.localScale.x * xScaleFactor,
                Model.transform.localScale.y * yScaleFactor,
                Model.transform.localScale.z * zScaleFactor
            );
            float SmallScale = FindSmallestValue(Model.transform.localScale.x, Model.transform.localScale.y, Model.transform.localScale.z);
            Model.transform.localScale = new Vector3(SmallScale, SmallScale, SmallScale);

        }
        Vector3 GetObjectSize(GameObject obj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                return renderer.bounds.size;
            }
            else
            {
                // If there is no Renderer component, return the local scale
                return obj.transform.localScale;
            }
        }
        float FindSmallestValue(float x, float y, float z)
        {
            // Compare a, b, and c to find the smallest value
            float smallest = Mathf.Min(x, Mathf.Min(y, z));
            return smallest;
        }
    }
}
