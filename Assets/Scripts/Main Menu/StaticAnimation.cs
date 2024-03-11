using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("StaticAlpha");
    }
    
    private void Update() 
    {
        if(GameManager.Instance.isStarting)
        {
            StopAllCoroutines();
        }    
    }
    IEnumerator StaticAlpha()
    {
        float timeBetweenSwitch;
        int randomNumber;
        Color auxColor = spriteRenderer.color;
        while(true)
        {
            timeBetweenSwitch = Random.Range(0.25f, 0.75f);
            randomNumber = Random.Range(0,2);
            yield return new WaitForSeconds(timeBetweenSwitch);
            if(randomNumber == 0) auxColor.a = Random.Range(0.25f, 0.3f);
            else if(randomNumber == 1) auxColor.a = Random.Range(0.1f,0.15f);
            spriteRenderer.color = auxColor;
        }
    }
}
