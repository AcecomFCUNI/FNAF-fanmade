using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : MonoBehaviour
{
    [SerializeField] private int hour;
    [SerializeField] private float hourDuration = 86;
    public int Hour 
    {
        get => hour;
        private set
        {
            hour = value;
            UIManager.Instance.UpdateHourText();    
        } 
    }
    private void Start() 
    {
        StartCoroutine(UpdateNightTime());
    }

    IEnumerator UpdateNightTime()
    {
        while(Hour != 6)
        {
            yield return new WaitForSeconds(hourDuration);
            Hour++;
        }
        GameManager.Instance.Win();
    }
}
