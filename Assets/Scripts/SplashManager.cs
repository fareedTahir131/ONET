using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(OnSplashEnd());
    }
    IEnumerator OnSplashEnd()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
