using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowPlane : MonoBehaviour
{
    [SerializeField] private Sprite hideImage;
    [SerializeField] private Sprite showImage;

    bool isHiding = true;

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ShowHidePlane);
    }

    void ShowHidePlane()
    {
        if (isHiding)
        {
            isHiding = false;
            button.image.sprite = showImage;
            foreach (var planeObj in PlaneRecognize.planeList)
            {
                planeObj.SetActive(true);
            }
        }
        else
        {
            isHiding = true;
            button.image.sprite = hideImage;
            foreach (var planeObj in PlaneRecognize.planeList)
            {
                planeObj.SetActive(false);
            }
        }
    }
}
