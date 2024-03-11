using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SurveillanceSystem : MonoBehaviour
{
    public static SurveillanceSystem Instance {get; private set;}

    [SerializeField] private Camera[] cameras;
    [SerializeField] private Camera officeCamera;
    [SerializeField] private Camera currentCamera;
    [SerializeField] private Canvas canvas;

    private Dictionary<string, Camera> surveillanceCameras;
    private Animator animator;
    private bool isCameraUp = false;
    private bool canToggle = true;

    private void Start() 
    {
        SingletonLogic();
        AssembleResources();
        animator = GetComponent<Animator>();
        currentCamera = surveillanceCameras["1A"];
    }

    private void Update() 
    {

    }

    private void SingletonLogic()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if (Instance != this) Destroy(gameObject);
    }

    private void AssembleResources()
    {
        surveillanceCameras = cameras.ToDictionary(x => x.name, x => x);
    }
    public void ToggleCamera()
    {
        if(!canToggle) return;
        if(isCameraUp)
        {
            isCameraUp = false;
            transform.position = new Vector3(currentCamera.transform.position.x, currentCamera.transform.position.y, transform.position.z);
            animator.Play("FlipDown");
        }
        else
        {
            isCameraUp = true;
            transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
            animator.Play("FlipUp");
            
        }
        StartCoroutine(WaitToToogle(0.75f));
    }

    private void UpdateCameraCanvas(Camera camera)
    {
        canvas.worldCamera = camera;
    }
    
    private void RaisingCameraMonitor()
    {
        currentCamera.enabled = true;
        officeCamera.enabled = false;
        UpdateCameraCanvas(currentCamera);
    }

    private void LoweringCameraMonitor()
    {
        transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
        officeCamera.enabled = true;
        currentCamera.enabled = false;
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
