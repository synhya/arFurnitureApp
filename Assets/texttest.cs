using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class texttest : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
