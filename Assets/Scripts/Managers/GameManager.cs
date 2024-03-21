using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lineAnimation;

    public static GameManager Instance {get; private set;}
    public int night = 1;
    public bool isStarting;
    public bool gameOver;
    
    [SerializeField]private GameObject nightObj;
    [SerializeField]private Image nightImage;
    [SerializeField]private GameObject chargingIcon;

    private void Awake() 
    {
        SetUpGame();
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }

    private void Update() 
    {
        QuitGame();
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameOver = false;
        nightObj = GameObject.FindWithTag("Night");
        chargingIcon = GameObject.FindWithTag("Charging Icon");
        nightImage = nightObj.GetComponent<Image>();
        nightObj.SetActive(false);       
        chargingIcon.SetActive(false);
    }

    private void SetUpGame()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void StartNight()
    {
        AudioListener.pause = true;
        GameObject obj = Instantiate(lineAnimation, transform.position, Quaternion.identity);
        Destroy(obj,3f);
        nightImage.sprite = GetNightImage.Instance.GetNightIm("N" + night);
        nightObj.SetActive(true);
        StartCoroutine(WaitThenLoad(4));
    }

    IEnumerator WaitThenLoad(float wait)
    {
        yield return new WaitForSeconds(wait);

        float time = 0;
        Color startValue = nightImage.color;
        Color targetValue = startValue;
        targetValue.a = 0;

        while(time < 1)
        {
            nightImage.color = Color.Lerp(startValue,targetValue, time/1);
            time += Time.deltaTime;
            yield return null;
        }
        nightImage.color = targetValue;

        chargingIcon.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    private void QuitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartNight();
        }
    }

    public void Win()
    {
        Debug.Log("Ganaste.");
    }
    
}
