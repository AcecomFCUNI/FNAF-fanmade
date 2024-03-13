using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private int id; // 0 is left, 1 is right
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private float waitToToggleDuration = 0.5f;
    private Animator buttonAnimator;
    private AudioSource audioSource;
    private bool isOpen = true;
    private bool canHitButton = true;
    

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        buttonAnimator = this.transform.parent.gameObject.GetComponent<Animator>();    
    }
    private void OnMouseDown() 
    {
        ToogleDoor();
    }

    private void ToogleDoor()
    {
        if(!canHitButton) return;
        if(isOpen)
        {
            isOpen = false;
            buttonAnimator.SetBool("isOpen", isOpen);
            doorAnimator.SetBool("isOpen", isOpen);
            audioSource.Play();
        }
        else
        {
            isOpen = true;
            buttonAnimator.SetBool("isOpen", isOpen);
            doorAnimator.SetBool("isOpen", isOpen);
            audioSource.Play();
        }
        StartCoroutine(WaitToToogle(waitToToggleDuration));
    }

    IEnumerator WaitToToogle(float duration)
    {
        float time = 0;
        canHitButton = false;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canHitButton = true;
    }
}
