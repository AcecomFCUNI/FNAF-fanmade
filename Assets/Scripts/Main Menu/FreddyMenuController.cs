using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreddyMenuController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("FreddyTwitching");
        StartCoroutine("FreddyAlpha");
    }
    
    private void Update() 
    {
        if(GameManager.Instance.isStarting)
        {
            StopAllCoroutines();
        }    
    }
    IEnumerator FreddyTwitching()
    {
        float timeBetweenTwitch = 0;
        while(true)
        {
            timeBetweenTwitch = Random.Range(0.5f, 2.5f);
            yield return new WaitForSeconds(timeBetweenTwitch);
            animator.SetFloat("animationSpeed", Random.Range(1,1.5f));
            animator.Play("Twitching");
        }
    }

    IEnumerator FreddyAlpha()
    {
        float timeBetweenSwitch;
        int randomNumber;
        Color auxColor = spriteRenderer.color;
        while(true)
        {
            timeBetweenSwitch = Random.Range(0.25f, 0.75f);
            randomNumber = Random.Range(0,2);
            yield return new WaitForSeconds(timeBetweenSwitch);
            if(randomNumber == 0) auxColor.a = Random.Range(0.4f,0.6f);
            else if(randomNumber == 1) auxColor.a = Random.Range(0.8f,1);
            spriteRenderer.color = auxColor;
        }
    }
}
