using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;

public class PlaneRecognize : MonoBehaviour
{
    public static List<GameObject> planeList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // if far enough
        WaitUIControl.Instance.PlaneRecognized();
        planeList.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }

}
