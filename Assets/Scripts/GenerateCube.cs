using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenerateCube : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject imagePrefab;
    [SerializeField]
    private GameObject placedPrefab;

    private GameObject tempImage;
    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }


    public void OnDrag(PointerEventData eventData)
    {
        // if (Utility.Raycast(eventData.position, out Pose hitPose))
        // {
        //     transform.position = hitPose.position;
        // }
        tempImage.transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        tempImage = Instantiate(imagePrefab, transform.position, transform.rotation, transform.parent);
        tempImage.transform.SetParent(canvas.transform, true);

        Image image = tempImage.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0.6f;
        image.color = tempColor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Generate Object
        if (Utility.Raycast(eventData.position, out Pose hitPose))
        {
            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
        }

        // delete dragging image
        Destroy(tempImage);
    }
}
