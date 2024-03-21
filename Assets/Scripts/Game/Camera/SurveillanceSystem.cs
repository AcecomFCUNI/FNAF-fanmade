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
    [SerializeField] private AudioSource fanAS;
    [SerializeField] private GameObject[] uICamera;
    [SerializeField] private Room[] rooms;
    [SerializeField] private GameObject[] cameraSounds;
    

    private Dictionary<string,Room> roomsDict;
    private Room currentRoom;
    private Animator animator;
    private AudioSource audioSource;
    private GameObject aux1;
    private GameObject aux2;
    private GameObject aux3;
    private bool isCameraUp = false;
    private bool canToggle = true;

    private void Start() 
    {
        AssembleResources();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentRoom = roomsDict["1A"];
    }

    public void ToggleCamera()
    {
        if(!canToggle) return;
        if(isCameraUp)
        {
            if(aux2 != null) Destroy(aux2);
            if(aux3 != null) Destroy(aux3);
            isCameraUp = false;
            transform.position = new Vector3(surveillanceCamera.transform.position.x, surveillanceCamera.transform.position.y, transform.position.z);
            fanAS.volume = 0.5f;
            aux1 = Instantiate(cameraSounds[0]);
            Destroy(aux1,5);
            animator.Play("FlipDown");
        }
        else
        {
            isCameraUp = true;
            transform.position = new Vector3(officeCamera.transform.position.x, officeCamera.transform.position.y, transform.position.z);
            fanAS.volume = 0.15f;
            aux2 = Instantiate(cameraSounds[1], surveillanceCamera.transform.position, Quaternion.identity);
            aux3 = Instantiate(cameraSounds[2], surveillanceCamera.transform.position, Quaternion.identity);
            Destroy(aux2,10);
            Destroy(aux3,20);
            animator.Play("FlipUp");
            
        }
        StartCoroutine(WaitToToogle(toggleDuration));
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
