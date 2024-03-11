using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();   
        StartCoroutine("Lines"); 
    }
    
    private void Update() 
    {
        if(GameManager.Instance.isStarting)
        {
            StopAllCoroutines();
        }    
    }

    IEnumerator Lines()
    {
        float timeBetweenAnimations;
        int randomNumber;
        while(true)
        {
            timeBetweenAnimations = Random.Range(1f, 2.5f);
            randomNumber = Random.Range(0,5);
            yield return new WaitForSeconds(timeBetweenAnimations);
            switch(randomNumber)
            {
                case 0:
                    animator.Play("1");
                    break;
                case 1:
                    animator.Play("2");
                    break;
                case 2:
                    animator.Play("3");
                    break;
                case 3:
                    animator.Play("4");
                    break;
            }
        }
    }

}
