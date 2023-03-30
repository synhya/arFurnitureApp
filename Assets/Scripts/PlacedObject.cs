using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class PlacedObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float outlineSize = 0.06f;
    [SerializeField]
    MeshRenderer[] meshRenderers;
    private float scaleSpeed = 1.0f;
    private float rotateSpeed = 60.0f;
    float prevTouchDist = 0f;

    float initTouchPositionX = 0f;

    public bool IsSelected
    {
        get => SelectedObject == this;
    }

    private static PlacedObject selectedObject;
    public static PlacedObject SelectedObject
    {
        get => selectedObject;
        set
        {
            if (selectedObject == value)
            {
                return;
            }

            if (selectedObject != null)
            {
                //
                foreach (var renderer in SelectedObject.meshRenderers)
                {
                    Material[] mats = renderer.materials;
                    foreach (var mat in mats)
                    {
                        mat.SetFloat("_Scale", 0f);
                    }
                }
            }

            selectedObject = value;

            if (value != null)
            {
                //
                foreach (var renderer in SelectedObject.meshRenderers)
                {
                    Material[] mats = renderer.materials;
                    foreach (var mat in mats)
                    {
                        mat.SetFloat("_Scale", selectedObject.outlineSize);
                    }
                }


                // favorite
                var info = SelectedObject.GetComponent<FurnitureInfo>();
                if (FavoriteListManager.Instance.favoriteDict[info.furnitureId].isFavorite == true)
                {
                    ButtonManager.Instance.ActivateFavButton(true);
                }
                else
                {
                    ButtonManager.Instance.ActivateFavButton(false);
                }
            }
            else
            {
                // selected == null
                ButtonManager.Instance.ActivateFavButton(false);
            }
        }
    }

    public void Awake()
    {
        foreach (var renderer in meshRenderers)
        {
            Material[] mats = renderer.materials;
            foreach (var mat in mats)
            {
                mat.SetFloat("_Scale", 0f);
            }
        }
        // SelectedObject = this; //for debug
    }

    public void Start()
    {
        // 정보 가져와서 크기 회전 조절
        var infoId = GetComponent<FurnitureInfo>();
        var info = FavoriteListManager.Instance.favoriteDict[infoId.furnitureId];
        transform.rotation = Quaternion.Euler(info.furnitureRotation);
        transform.localScale = info.furnitureScale;
        infoId.isFavorite = FavoriteListManager.Instance.favoriteDict[infoId.furnitureId].isFavorite;

    }

    public static Utility.ControlMode controlMode = Utility.ControlMode.baseMode;

    public void OnDrag(PointerEventData eventData)
    {
        if (IsSelected && controlMode == Utility.ControlMode.baseMode)
        {
            if (Utility.Raycast(eventData.position, out Pose hitPose))
            {
                transform.position = hitPose.position;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 쓰레기 통이면 제거
        if (IsSelected && controlMode == Utility.ControlMode.baseMode)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            if (results.Count > 0)
            {
                if (results[0].gameObject.tag == "Delete")
                {
                    // fav 버튼 active면 false로 변경
                    ButtonManager.Instance.favoriteButton.image.sprite = ButtonManager.Instance.favoriteOriginSprite;

                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void Update()
    {
        if (IsSelected && controlMode == Utility.ControlMode.scaleMode)
        {
            ManageScale();

        }
        else if (IsSelected && controlMode == Utility.ControlMode.rotateMode)
        {
            ManageRotate();
        }

    }

    private void ManageScale()
    {
        if (Input.touchCount >= 2)
        {
            float touchDist = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            if (prevTouchDist == 0)
            {
                prevTouchDist = touchDist;
                return;
            }
            if (prevTouchDist < touchDist)
            {
                Vector3 spawnScale = transform.localScale;
                transform.localScale = new Vector3(
                    spawnScale.x + 1f * scaleSpeed * Time.deltaTime,
                    spawnScale.y + 1f * scaleSpeed * Time.deltaTime,
                    spawnScale.z + 1f * scaleSpeed * Time.deltaTime);
            }
            else if (prevTouchDist > touchDist)
            {
                Vector3 spawnScale = transform.localScale;
                transform.localScale = new Vector3(
                    spawnScale.x - 1f * scaleSpeed * Time.deltaTime,
                    spawnScale.y - 1f * scaleSpeed * Time.deltaTime,
                    spawnScale.z - 1f * scaleSpeed * Time.deltaTime);
            }
            prevTouchDist = touchDist;
        }
        else
        {
            prevTouchDist = 0;
        }
    }

    private void ManageRotate()
    {
        if (Input.touchCount >= 1)
        {
            var touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                initTouchPositionX = touch.position.x;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.position.x - initTouchPositionX;
                if (deltaX > 0)
                {

                    transform.RotateAround(transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
                }
                else
                {
                    transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
                }
            }
            // if(touch.phase == TouchPhase.Ended)

        }
    }
}
