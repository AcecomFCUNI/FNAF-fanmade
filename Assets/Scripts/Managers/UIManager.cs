using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private Text hourText;
    [SerializeField] private Night night;

    private void Start()
    {
        SetUpGame();    
    }

    private void SetUpGame()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if (Instance != this) Destroy(gameObject);
    }

    public void UpdateHourText()
    {
        if(night.Hour == 0) hourText.text = "12 AM";
        else  hourText.text = night.Hour.ToString() + " AM";
    }
}
