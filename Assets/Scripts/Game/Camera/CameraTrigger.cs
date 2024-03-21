using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraTrigger : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private SurveillanceSystem surveillanceSystem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        surveillanceSystem.ToggleCamera();
    }
}
