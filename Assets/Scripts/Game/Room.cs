using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string camId;
    public string roomName;
    public string CamId
    {
        get => camId;
        set => camId = value;
    }
}
