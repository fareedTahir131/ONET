using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void Load_Scene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void ChangeOrientation(int SceneIndex)
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(SceneIndex);
    }
}
