using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
{
    [SerializeField] private GameObject cursor;    
    private Vector2 cursorNextPosition;
    private Vector2 cursorInitialPosition;

    private RectTransform rectTransform;
    private AudioSource audioSource;
    private void Start() 
    {
        rectTransform = cursor.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        cursorInitialPosition = rectTransform.anchoredPosition;
        cursorNextPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(-40, 0);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(rectTransform.anchoredPosition != cursorNextPosition && !GameManager.Instance.isStarting) 
        {
            audioSource.Play();
            rectTransform.anchoredPosition = cursorNextPosition;
        }
        
        
    }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     rectTransform.anchoredPosition = cursorInitialPosition;
    // }
}
