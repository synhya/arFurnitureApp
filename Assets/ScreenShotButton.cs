using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScreenShotButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;

    public Sprite newSprite;
    public Sprite originSprite;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClickCaptureScreen);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button.image.sprite = newSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button.image.sprite = originSprite;
    }

    public void OnClickCaptureScreen()
    {
        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen()
    {
        yield return null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;

        yield return new WaitForEndOfFrame();

        TakeShot();

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }

    void TakeShot()
    {
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);
        if (permission == NativeGallery.Permission.Denied)
        {
            if (NativeGallery.CanOpenSettings())
            {
                NativeGallery.OpenSettings();
            }
        }


        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "BRUNCH-SCREENSHOT-" + timestamp + ".png";
        string albumName = "AR";
        NativeGallery.SaveImageToGallery(texture, albumName, fileName);

        Object.Destroy(texture);
    }


}
