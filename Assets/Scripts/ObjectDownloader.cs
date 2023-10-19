using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using TriLibCore.Samples;

public class ObjectDownloader : MonoBehaviour
{
    public string objURL = "https://onet-bucket.s3.us-west-1.amazonaws.com/8j8cpggegc4znm7a1ejyqw7nahrl?response-content-disposition=attachment%3B%20filename%3D%2212228_Dog_v1_L2.obj%22%3B%20filename%2A%3DUTF-8%27%2712228_Dog_v1_L2.obj&response-content-type=model%2Fobj&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIARYY2LLNHK3MIYNEE%2F20231016%2Fus-west-1%2Fs3%2Faws4_request&X-Amz-Date=20231016T045200Z&X-Amz-Expires=300&X-Amz-SignedHeaders=host&X-Amz-Signature=a19b5a8462a0a87d699f0dcac51b06c526a97830ae621fc7f98faa6214172a3d";
    public string imageUrl = "URL_TO_YOUR_IMAGE";

    public LoadModelFromFileSample loadModelFromFileSample;
    private string localFilePath;

    private bool IsModelDownloading = false;
    private bool IsModelDownloaded = false;
    private bool IsModelLinkFound = false;


    private string Model_Url = "";
    private string Texture_Url = "";

    private string Model_Path = "";
    private string Texture_Path = "";

    private void Start()
    {
        IsModelDownloaded = false;
        IsModelDownloading = false;
        IsModelLinkFound = false;
        //DownloadModel(objURL, imageUrl);
    }
    public void DownloadModel(string ModelUrl, string TextureUrl)
    {
        Model_Url = ModelUrl;
        Texture_Url = TextureUrl;
        IsModelLinkFound = true;
        IsModelDownloading = true;
        StartCoroutine(ModelDownloader(ModelUrl, TextureUrl));
    }
    public void ModelTargetFound()
    {
        if (IsModelLinkFound)
        {
            if (loadModelFromFileSample.ModelLoaded)
            {
                Debug.Log("Only show the model");
            }
            else if (IsModelDownloaded)
            {
                loadModelFromFileSample.LoadModelFromPath(Model_Path, Texture_Path);
            }
            else
            {
                StopCoroutine(ModelDownloader(Model_Url, Texture_Url));
            }
        }
    }
    public void ModelTargetLost()
    {
        if (IsModelLinkFound)
        {
            if (IsModelDownloading)
            {
                StopCoroutine(ModelDownloader(Model_Url, Texture_Url));
            }
        }
    }
    IEnumerator ModelDownloader(string ModelUrl, string TextureUrl)
    {
        // Create a unique file name based on the current time to avoid overwriting existing files
        string fileName = "Model.obj";
        localFilePath = Path.Combine(Application.persistentDataPath, fileName);

        // Start downloading the OBJ file
        using (UnityWebRequest www = UnityWebRequest.Get(ModelUrl))
        {
            www.downloadHandler = new DownloadHandlerBuffer(); // Use DownloadHandlerBuffer to get data as byte array

            yield return www.SendWebRequest();

            while (!www.isDone)
            {
                float progress = www.downloadProgress;
                Debug.Log("Downloading... " + (progress * 100f).ToString("F2") + "%");
                yield return null;
            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error downloading OBJ file: " + www.error);
            }
            else
            {
                if (File.Exists(localFilePath))
                {
                    File.Delete(localFilePath);
                    Debug.Log("Old Model deleted.");
                }
                // Save the downloaded OBJ file to the persistent data path
                File.WriteAllBytes(localFilePath, www.downloadHandler.data);
                Debug.Log("OBJ file downloaded and saved to: " + localFilePath);

                // Now you can load the OBJ model into the scene if needed
                //LoadObjModel();
                //loadModelFromFileSample.LoadModelFromPath(localFilePath);
                StartCoroutine(TextureDownloader(localFilePath, TextureUrl));
            }
        }
    }

    IEnumerator TextureDownloader(string ModelPath, string TextureUrl)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(TextureUrl))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error downloading image: " + www.error);
            }
            else
            {
                // Get the downloaded texture
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                // Convert the texture to a byte array
                byte[] bytes = texture.EncodeToPNG();

                // Create a unique file name based on current time to avoid overwriting existing files
                string fileName = "ModelTexture.png";
                string localFilePath = Path.Combine(Application.persistentDataPath, fileName);

                // Check if the file already exists, and delete it if it does
                if (File.Exists(localFilePath))
                {
                    File.Delete(localFilePath);
                    Debug.Log("Old image deleted.");
                }

                // Save the new image to the persistent data path
                File.WriteAllBytes(localFilePath, bytes);

                Debug.Log("Image downloaded and saved to: " + localFilePath);
                IsModelDownloading = false;
                IsModelDownloaded = true;
                Model_Path = ModelPath;
                Texture_Path = localFilePath;
                loadModelFromFileSample.LoadModelFromPath(ModelPath, localFilePath);
            }
        }
    }
    
}
