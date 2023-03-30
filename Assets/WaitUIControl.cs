using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUIControl : MonoBehaviour
{
    private static WaitUIControl instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static WaitUIControl Instance
    {
        get
        {
            if (instance == null)
                return null;
            else
                return instance;
        }
    }

    public CanvasGroup waitAlpha;
    public RectTransform doneTransfrom;
    float originalPointY;
    float reachingPointY;

    public bool isWaiting = true;
    // Start is called before the first frame update
    void Start()
    {
        waitAlpha.alpha = 0f;
        originalPointY = doneTransfrom.localPosition.y;
        reachingPointY = waitAlpha.transform.localPosition.y;
    }



    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
            waitAlpha.alpha = Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / 1.5f, 1f));
    }

    public void PlaneRecognized()
    {
        if (!isWaiting)
            return;
        isWaiting = false;
        waitAlpha.LeanAlpha(0f, 0.5f).setEaseOutExpo();
        StartCoroutine(DestroyWait());

        doneTransfrom.LeanMoveLocalY(reachingPointY, 1f).setEaseOutExpo().delay = 0.2f;
        doneTransfrom.LeanMoveLocalY(originalPointY, 1f).setEaseInExpo().delay = 2.2f;
    }

    IEnumerator DestroyWait()
    {
        yield return new WaitForSeconds(1);

        Destroy(waitAlpha.gameObject);
    }
}
