using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetNightImage : MonoBehaviour
{
    public static GetNightImage Instance {get; private set;}

    [SerializeField] private List<Sprite> nightsImages;
    [SerializeField] private Dictionary<string,Sprite> nightsImagesDict;
    private void Awake()
    {
        SingletonLogic();  
        AssembleResources();      
    }

    private void SingletonLogic()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void AssembleResources()
    {
        nightsImages = Resources.LoadAll<Sprite>("NightImages").ToList();
        nightsImagesDict = nightsImages.ToDictionary(x => x.name, x => x);
    }

    public Sprite GetNightIm(string name)
    {
        return nightsImagesDict[name];
    }
}
