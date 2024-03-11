using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private int id; // 0 is left, 1 is right
    [SerializeField] private Animator doorAnimator;
    private Animator buttonAnimator;
    private AudioSource audioSource;
    private bool isOpen = true;
    
    

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
    }
}
