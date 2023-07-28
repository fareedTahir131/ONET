using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;

        }
    }
    public void SlideInMenuAnim(Image image)
    {
        image.transform.DOLocalMove(new Vector3(0,0, 0), 0.5f).SetEase(Ease.OutBack);
    }
    public void SlideOutMenuAnim(Image image)
    {
        image.transform.DOLocalMove(new Vector3(-1080, 0, 0), 0.5f).SetEase(Ease.OutBack);
    }
    public void FadeInAndOut(Image Screen)
    {
        Screen.GetComponent<CanvasGroup>().DOFade((1 - Screen.GetComponent<CanvasGroup>().alpha), 0.5f);
    }
}
