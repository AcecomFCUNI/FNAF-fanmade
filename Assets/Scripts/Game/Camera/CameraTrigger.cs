using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraTrigger : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private SurveillanceSystem surveillanceSystem;
    private AudioSource audioSource;
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(surveillanceSystem.ToggleCamera()) audioSource.Play();
    }
}
