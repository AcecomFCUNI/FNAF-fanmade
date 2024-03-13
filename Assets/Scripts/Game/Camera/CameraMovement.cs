using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float movementThreshold = 4f;
    public float startPoint = 2.5f; //if the mouse has x position greater than |this|, then the camera will move
    public float mouseThreshold = 5f;

    private float distance = 0f;
    private Vector2 mousePosition;
    
    private void Update() 
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if(!GetComponent<Camera>().enabled) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float max = mouseThreshold - startPoint;                  // 5 - 2.5 = 2.5 => 100%
        float speed = Mathf.Abs(mousePosition.x) - startPoint; // 3 - 2.5 = 0.5 => ?%

        speed = speed / max;                                    // 0.5 / 2.5 = 0.2 => 20%


        if (mousePosition.x > startPoint)
        {
            distance += movementSpeed * speed * Time.deltaTime;
        }
        else if (mousePosition.x < -startPoint)
        {
            distance -= movementSpeed * speed * Time.deltaTime;
        }

        distance = Mathf.Clamp(distance, -movementThreshold, movementThreshold);
        transform.position = new Vector3(distance, 0f, transform.position.z);
    }
}
