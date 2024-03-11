using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup uIGroup;
    [SerializeField] private SpriteRenderer blackScreen;
    [SerializeField] private float timeToFadeOut = 3;
    [SerializeField] private float timeForNewspaper = 5;
    [SerializeField] private GameObject newspaper;
    [SerializeField] private SpriteRenderer[] menuItems;

    private void Start() {
        AudioListener.pause = false;    
    }
    public void NewGame()
    {
        GameManager.Instance.isStarting = true;
        GameManager.Instance.night = 1;
        newspaper.SetActive(true);
        foreach ( SpriteRenderer item in menuItems)
        {
            StartCoroutine(FadeOutGameObjectFunction(timeToFadeOut, item));
        }
        StartCoroutine(FadeOutCanva(0.75f,timeToFadeOut,uIGroup));
        StartCoroutine(FadeInBlackScreen(timeToFadeOut + timeForNewspaper, 2));
    }

    public void Continue()
    {
        GameManager.Instance.isStarting = true;
        Color auxColor;
        foreach(SpriteRenderer item in menuItems)
        {
            auxColor = item.color;
            auxColor.a = 0;
            item.color = auxColor;
        }
        uIGroup.alpha = 0;
        GameManager.Instance.StartNight();
    }
    IEnumerator FadeOutGameObjectFunction(float duration, SpriteRenderer item)
    {
        float time = 0;
        Color startValue = item.color;
        Color targetValue = startValue;
        targetValue.a = 0;

        while(time < duration)
        {
            item.color = Color.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        item.color = targetValue;
    }

    IEnumerator FadeOutCanva(float waitForSeconds,float duration, CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(waitForSeconds);
        float time = 0;
        
        while(time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1,0,time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    IEnumerator FadeInBlackScreen(float waitToDo, float duration)
    {
        yield return new WaitForSeconds(waitToDo);
        float time = 0;
        Color startValue = blackScreen.color;
        Color targetValue = startValue;
        targetValue.a = 1;

        while(time < duration)
        {
            blackScreen.color = Color.Lerp(startValue,targetValue, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = targetValue;
        GameManager.Instance.StartNight();
    }
}
