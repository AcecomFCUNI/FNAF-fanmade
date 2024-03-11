using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    [SerializeField] private int id; // 0 is left, 1 is right
    [SerializeField] private Animator officeAnimator;
    [SerializeField] private LightButton otherButton;
    private AudioSource audioSource;
    private Animator buttonAnimator;
    private bool isOn;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        buttonAnimator = this.transform.parent.gameObject.GetComponent<Animator>();   
    }

    private void OnMouseDown() 
    {
        ToogleLights();
    }

    private void CheckOtherButton()
    {
        if(otherButton.isOn)
        {
            otherButton.isOn = false;
            otherButton.buttonAnimator.SetBool("isOn", otherButton.isOn);
            otherButton.audioSource.Stop();
        }
    }

    private void ToogleLights()
    {
        if(isOn)
        {
            isOn = false;
            buttonAnimator.SetBool("isOn", isOn);
            officeAnimator.SetBool("isOn", isOn);
            officeAnimator.SetInteger("id", id);
            audioSource.Stop();
        }
        else
        {
            CheckOtherButton();
            isOn = true;
            buttonAnimator.SetBool("isOn", isOn);
            officeAnimator.SetBool("isOn", isOn);
            officeAnimator.SetInteger("id", id);
            audioSource.Play();
            StartCoroutine(DoLightsAnimation(3));
        }
    }

    IEnumerator DoLightsAnimation(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            if(!isOn) break;
            officeAnimator.SetFloat("randomNumber", Random.Range(0, 40));
            time += Time.deltaTime;
            yield return null;
        }
        isOn = false;
        buttonAnimator.SetBool("isOn", isOn);
        officeAnimator.SetBool("isOn", otherButton.isOn);
        audioSource.Stop();
    }
}
