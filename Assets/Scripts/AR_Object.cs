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


    private void Start()
    {
        canvas.worldCamera = Camera.main;
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
