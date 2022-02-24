using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

public class AR_Object : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Text titleText;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    public void SetTitle(string str)
    {
        titleText.text = str;
    }


    void videoOnClick()
    {
        GameManager.instance.SelectVideo(true);
    }

    void imageOnClick()
    {
        GameManager.instance.SelectImage(true);
    }
    
}
