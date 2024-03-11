using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAudioPause : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
    }
}
