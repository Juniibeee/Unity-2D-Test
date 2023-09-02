using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    public Image fadeScreen;

    [SerializeField] private float fadeSpeed;
    private bool shouldFadeToBlack;
    private bool shouldFadeFromBlack;
    // Declare each coroutine to prevent any potential overlap using StopCoroutine
    private IEnumerator fadeRoutine;

    private void Start() { 
        FadeToClear(); 
    }
    
    // Used in Area Entrance/Exit Scripts
    public void FadeToBlack() 
    {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(fadeScreen, 1);
        StartCoroutine(fadeRoutine);
    }
    
    // Used in Area Entrance/Exit Scripts
    public void FadeToClear()
    {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
        }
        
        fadeRoutine = FadeRoutine(fadeScreen, 0);
        StartCoroutine(fadeRoutine);
    }
    
    private IEnumerator FadeRoutine(Image image, float targetAlpha)
    {
        while(!Mathf.Approximately(image.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(image.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}