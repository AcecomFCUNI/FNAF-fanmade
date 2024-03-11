using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] private string cameraName;
    [SerializeField] private float moveThreshold;
    [SerializeField] private float speed;

    private Vector3[] waypoints = new Vector3[2];
    private GameObject staticImage;
    private Camera camera;
    private int index = 0;
    private bool isWaiting;

    private void Start() 
    {
        camera = GetComponent<Camera>();
        waypoints[0] = new Vector3(transform.position.x + moveThreshold, transform.position.y, transform.position.z);
        waypoints[1] = new Vector3(transform.position.x - moveThreshold, transform.position.y, transform.position.z);
    }

    private void Update() 
    {
        MoveCamera();
    }
    private void ActivateCamera()
    {

    }

    private void DeactivateCamera()
    {

    }

    private void MoveCamera()
    {
        if(!camera.enabled || isWaiting) return;
        
        transform.position = Vector3.MoveTowards(transform.position, waypoints[index],Time.deltaTime * speed);

        if(transform.position == waypoints[index]) 
        {
            StartCoroutine(WaitThenKeepMoving(1.5f));
            index = (index + 1)%2;
        }
    }

    IEnumerator WaitThenKeepMoving(float duration)
    {
        isWaiting = true;
        yield return new WaitForSeconds(duration);
        isWaiting = false;
    }
}
