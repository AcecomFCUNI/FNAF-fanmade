using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NightCall : MonoBehaviour
{
    public static NightCall Instance {get; private set;}

    [SerializeField] private List<AudioClip> nightsCalls;
    [SerializeField] private Dictionary<string,AudioClip> nightsCallsDict;
    private void Awake()
    {
        SingletonLogic();  
        AssembleResources();      
    }

    private void SingletonLogic()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void AssembleResources()
    {
        nightsCalls = Resources.LoadAll<AudioClip>("Night Calls").ToList();
        nightsCallsDict = nightsCalls.ToDictionary(x => x.name, x => x);
    }

    public AudioClip GetNightCall(string name)
    {
        return nightsCallsDict[name];
    }
}
