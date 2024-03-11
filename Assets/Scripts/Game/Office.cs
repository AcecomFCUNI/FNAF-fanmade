using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Office : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start() 
    {
        AudioListener.pause = false;
        audioSource = GetComponent<AudioSource>();    
    }

    private void Update() 
    {
        BackToMenu();
    }

    private void OnMouseDown()
    {
        audioSource.Play();
    }
    
    private void BackToMenu()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(0);
        }
    }
}
