using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class ImageDownloader : MonoBehaviour
{
    public string imageUrl = "URL_TO_YOUR_IMAGE"; // Replace this with the URL of the image you want to download

    IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
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
            }
        }
    }
}
