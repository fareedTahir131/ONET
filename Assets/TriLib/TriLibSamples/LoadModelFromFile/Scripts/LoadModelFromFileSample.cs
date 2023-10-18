#pragma warning disable 649
using TriLibCore.General;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace TriLibCore.Samples
{
    /// <summary>
    /// Represents a sample that loads the "TriLibSample.obj" Model from the "Models" folder.
    /// </summary>
    public class LoadModelFromFileSample : MonoBehaviour
    {
        private string ModelTextureUrl;
#if UNITY_EDITOR
        /// <summary>
        /// The Model asset used to locate the filename when running in Unity Editor.
        /// </summary>
        [SerializeField]
        private Object ModelAsset;
#endif

        /// <summary>
        /// Returns the path to the "TriLibSample.obj" Model.
        /// </summary>
        private string ModelPath
        {
            get
            {
                //#if UNITY_EDITOR
                //                return AssetDatabase.GetAssetPath(ModelAsset);
                //#else
                //                return "Models/TriLibSampleModel.obj";
                //#endif
                string fileName = "Model.obj";
                string localFilePath;
                localFilePath = Path.Combine(Application.persistentDataPath, fileName);
                Debug.Log("localFilePath " + localFilePath);
                return localFilePath;
            }
        }

        /// <summary>
        /// Loads the "Models/TriLibSample.obj" Model using the given AssetLoaderOptions.
        /// </summary>
        /// <remarks>
        /// You can create the AssetLoaderOptions by right clicking on the Assets Explorer and selecting "TriLib->Create->AssetLoaderOptions->Pre-Built AssetLoaderOptions".
        /// </remarks>
        private void Start()
        {
            //var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            //AssetLoader.LoadModelFromFile(ModelPath, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions);
        }

        public void LoadModelFromPath(string Model_Path, string TexturePath)
        {
            ModelTextureUrl = TexturePath;
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            AssetLoader.LoadModelFromFile(Model_Path, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions);
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
            LoadImageAndAssignTexture();
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

        void LoadImageAndAssignTexture()
        {
            // Check if the image file exists
            if (System.IO.File.Exists(ModelTextureUrl))
            {
                // Load the PNG image as a byte array
                byte[] fileData = System.IO.File.ReadAllBytes(ModelTextureUrl);

                // Create a new texture and load the image data into it
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData); // LoadImage automatically resizes the texture dimensions
                GameObject ParentModel = GameObject.Find("Model");
                GameObject model = ParentModel.transform.GetChild(0).gameObject;
                // Assign the texture to the material of the model
                if (model != null)
                {
                    Renderer renderer = model.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.mainTexture = texture;
                    }
                    else
                    {
                        Debug.LogError("Model does not have a Renderer component.");
                    }
                }
                else
                {
                    Debug.LogError("Model GameObject reference is not set.");
                }
            }
            else
            {
                Debug.LogError("Image file not found at path: " + ModelTextureUrl);
            }
        }
    }
}
