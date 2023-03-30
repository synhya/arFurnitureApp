using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button button;

    public Sprite originSprite;
    public Sprite newSprite;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.sprite = newSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.sprite = originSprite;
    }
}
