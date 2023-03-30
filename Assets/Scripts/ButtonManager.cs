using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private static ButtonManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static ButtonManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public Button rotateButton;
    public Button scaleButton;
    public Button favoriteButton;

    public Sprite rotateOriginSprite;
    public Sprite rotateNewSprite;
    public Sprite scaleOriginSprite;
    public Sprite scaleNewSprite;
    public Sprite favoriteOriginSprite;
    public Sprite favoriteNewSprite;



    public void ObjectModeToRotate()
    {
        if (PlacedObject.controlMode == Utility.ControlMode.rotateMode)
        {
            PlacedObject.controlMode = Utility.ControlMode.baseMode;
            rotateButton.image.sprite = rotateOriginSprite;
            SaveOnClick();
        }
        else if (PlacedObject.SelectedObject != null)
        {
            PlacedObject.controlMode = Utility.ControlMode.rotateMode;
            rotateButton.image.sprite = rotateNewSprite;
            scaleButton.image.sprite = scaleOriginSprite;

            SaveOnClick();
        }
    }
    public void ObjectModeToScale()
    {
        if (PlacedObject.controlMode == Utility.ControlMode.scaleMode)
        {
            PlacedObject.controlMode = Utility.ControlMode.baseMode;
            scaleButton.image.sprite = scaleOriginSprite;
            SaveOnClick();
        }
        else if (PlacedObject.SelectedObject != null)
        {
            PlacedObject.controlMode = Utility.ControlMode.scaleMode;
            rotateButton.image.sprite = rotateOriginSprite;
            scaleButton.image.sprite = scaleNewSprite;

            SaveOnClick();
        }

        // save in firebase
    }

    public void SaveOnClick()
    {
        // save in firebase on click
        FurnitureInfo info = PlacedObject.SelectedObject.GetComponent<FurnitureInfo>();
        info.furnitureRotation = PlacedObject.SelectedObject.transform.rotation.eulerAngles;
        info.furnitureScale = PlacedObject.SelectedObject.transform.localScale;

        FirebaseManager.Instance.WriteInfoData(info);
        // save in local dict
        FavoriteListManager.Instance.favoriteDict[info.furnitureId] = info;
    }

    public void AddRemoveFavorite()
    {
        if (PlacedObject.SelectedObject != null)
        {
            var info = PlacedObject.SelectedObject.GetComponent<FurnitureInfo>();
            if (FavoriteListManager.Instance.favoriteDict[info.furnitureId].isFavorite == true)
            {
                FavoriteListManager.Instance.favoriteDict[info.furnitureId].isFavorite = false;
                favoriteButton.image.sprite = favoriteOriginSprite;
            }
            else
            {
                FavoriteListManager.Instance.favoriteDict[info.furnitureId].isFavorite = true;
                favoriteButton.image.sprite = favoriteNewSprite;
            }
            info.isFavorite = FavoriteListManager.Instance.favoriteDict[info.furnitureId].isFavorite;
            FirebaseManager.Instance.WriteInfoData(info);
        }
    }

    public void ActivateFavButton(bool activate)
    {
        if (activate)
        {
            favoriteButton.image.sprite = favoriteNewSprite;
        }
        else
        {
            favoriteButton.image.sprite = favoriteOriginSprite;
        }
    }
}
