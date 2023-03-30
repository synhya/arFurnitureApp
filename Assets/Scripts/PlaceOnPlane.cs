using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private LayerMask placedObjectLayerMask;
    private Vector2 touchPosition;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField]
    GraphicRaycaster raycaster;

    private void Update()
    {
        if (!Utility.TryGetInputPosition(out touchPosition)) return;

        if (PlacedObject.controlMode == Utility.ControlMode.baseMode)
        {
            ray = arCamera.ScreenPointToRay(touchPosition);

            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = touchPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);

            if (results.Count == 0)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, placedObjectLayerMask))
                {
                    PlacedObject.SelectedObject = hit.transform.GetComponentInParent<PlacedObject>();
                    return;
                }
                // not interacting with UI
                PlacedObject.SelectedObject = null;
            }
        }
    }
}
