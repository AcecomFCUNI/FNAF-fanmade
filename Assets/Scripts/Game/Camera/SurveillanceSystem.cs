using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SurveillanceSystem : MonoBehaviour
{
    public static SurveillanceSystem Instance {get; private set;}

    [SerializeField] private Camera officeCamera;
    [SerializeField] private Camera surveillanceCamera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject[] uICamera;
    [SerializeField] private float toggleDuration;

    private Animator animator;
    private bool isCameraUp = false;
    private bool canToggle = true;

    private void Start() 
    {
        SingletonLogic();
        animator = GetComponent<Animator>();
    }

    private void SingletonLogic()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if (Instance != this) Destroy(gameObject);
    }

    public void ToggleCamera()
    {
        if(!canToggle) return;
        if(isCameraUp)
        {
            isCameraUp = false;
            transform.position = new Vector3(surveillanceCamera.transform.position.x, surveillanceCamera.transform.position.y, transform.position.z);
            animator.Play("FlipDown");
        }
        else
        {
            isCameraUp = true;
            transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
            animator.Play("FlipUp");
            
        }
        StartCoroutine(WaitToToogle(toggleDuration));
    }

    private void UpdateCameraCanvas(Camera camera)
    {
        canvas.worldCamera = camera;
    }
    
    private void RaisingCameraMonitor()
    {
        surveillanceCamera.enabled = true;
        officeCamera.enabled = false;
        foreach(GameObject uIItem in uICamera)
        {
            uIItem.SetActive(true);
        }
        UpdateCameraCanvas(surveillanceCamera);
    }

    private void LoweringCameraMonitor()
    {
        transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
        officeCamera.enabled = true;
        surveillanceCamera.enabled = false;
        foreach(GameObject uIItem in uICamera)
        {
            uIItem.SetActive(false);
        }
        UpdateCameraCanvas(officeCamera);
    }

    IEnumerator WaitToToogle(float duration)
    {
        float time = 0;
        canToggle = false;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canToggle = true;
    }
}
