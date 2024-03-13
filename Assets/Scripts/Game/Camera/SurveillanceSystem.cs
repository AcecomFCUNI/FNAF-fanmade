using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SurveillanceSystem : MonoBehaviour
{
    [SerializeField] private Camera officeCamera;
    [SerializeField] private SurveillanceCamera surveillanceCamera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector3 auxPosition;
    [SerializeField] private float toggleDuration;
    [SerializeField] private GameObject[] uICamera;
    [SerializeField] private Room[] rooms;
    [SerializeField] private AudioSource fanAS;
    
    private Dictionary<string,Room> roomsDict;
    private Room currentRoom;
    private Animator animator;
    private AudioSource audioSource;
    private bool isCameraUp = false;
    private bool canToggle = true;

    private void Start() 
    {
        AssembleResources();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentRoom = roomsDict["1A"];
    }

    public bool ToggleCamera()
    {
        if(!canToggle) return canToggle;
        if(isCameraUp)
        {
            isCameraUp = false;
            transform.position = new Vector3(surveillanceCamera.transform.position.x, surveillanceCamera.transform.position.y, transform.position.z);
            fanAS.volume = 0.8f;
            animator.Play("FlipDown");
        }
        else
        {
            isCameraUp = true;
            transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
            fanAS.volume = 0.35f;
            animator.Play("FlipUp");
            
        }
        StartCoroutine(WaitToToogle(toggleDuration));
        return true;
    }

    private void AssembleResources()
    {
        roomsDict = rooms.ToDictionary( x => x.CamId, x => x);
    }

    private void UpdateCameraCanvas(Camera camera)
    {
        canvas.worldCamera = camera;
    }
    
    private void RaisingCameraMonitor()
    {
        surveillanceCamera.ActivateCamera();
        officeCamera.enabled = false;
        foreach(GameObject uIItem in uICamera)
        {
            uIItem.SetActive(true);
        }
        UpdateCameraCanvas(surveillanceCamera.camera);
    }

    private void LoweringCameraMonitor()
    {
        transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
        officeCamera.enabled = true;
        surveillanceCamera.DeactivateCamera();
        foreach(GameObject uIItem in uICamera)
        {
            uIItem.SetActive(false);
        }
        UpdateCameraCanvas(officeCamera);
    }

    public void ChangeCamera(string camId)
    {
        audioSource.Play();
        UIManager.Instance.ChangeCameraAnimation();
        if(currentRoom.CamId == camId) return;
        Room room = roomsDict[camId];
        UIManager.Instance.UpdateRoomNameText(room.roomName);
        room.transform.position = surveillanceCamera.initialPosition;
        currentRoom.transform.position = auxPosition;
        currentRoom = room;
        
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
