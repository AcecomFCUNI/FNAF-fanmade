using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAudio : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = NightCall.Instance.GetNightCall("C" + GameManager.Instance.night);
        audioSource.Play();
        
    }

}
