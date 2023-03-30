using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCategory : MonoBehaviour
{
    [SerializeField]
    Transform category;
    [SerializeField]
    Transform iconBar;

    Button button;

    [SerializeField]
    bool isReturning = false;

    float toIcon, toCate;

    void Start()
    {
        button = GetComponent<Button>();
        toIcon = iconBar.transform.localPosition.y;
        toCate = category.transform.localPosition.y;
        if (isReturning)
            button.onClick.AddListener(ReturnToIcon);
        else
            button.onClick.AddListener(ShowCate);
    }

    void ShowCate()
    {
        iconBar.transform.LeanMoveLocalY(toCate, 0.5f).setEaseInCirc();
        category.transform.LeanMoveLocalY(toIcon, 0.5f).setEaseInCirc().delay = 0.5f;
    }

    void ReturnToIcon()
    {
        iconBar.transform.LeanMoveLocalY(toIcon, 0.5f).setEaseInCirc().delay = 0.5f;
        category.transform.LeanMoveLocalY(toCate, 0.5f).setEaseInCirc();
    }
}
