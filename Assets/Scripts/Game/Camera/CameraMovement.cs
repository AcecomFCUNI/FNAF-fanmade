using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement configuration")]
    public float velocidadMovimiento = 4f;
    public float margenMovimiento = 4f;
    public float margenMouse = 2.5f;
    public float limiteMouse = 5f;

    private float movimiento = 0f;
    private Vector2 mousePosition;
    
    private void Update() 
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if(!GetComponent<Camera>().enabled) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float max = limiteMouse - margenMouse;                  // 5 - 2.5 = 2.5 => 100%
        float speed = Mathf.Abs(mousePosition.x) - margenMouse; // 3 - 2.5 = 0.5 => ?%

        speed = speed / max;                                    // 0.5 / 2.5 = 0.2 => 20%


        if (mousePosition.x > margenMouse)
        {
            movimiento += velocidadMovimiento * speed * Time.deltaTime;
        }
        else if (mousePosition.x < -margenMouse)
        {
            movimiento -= velocidadMovimiento * speed * Time.deltaTime;
        }

        movimiento = Mathf.Clamp(movimiento, -margenMovimiento, margenMovimiento);
        transform.position = new Vector3(movimiento, 0f, transform.position.z);
    }
}
