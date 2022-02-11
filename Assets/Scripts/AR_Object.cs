using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AR_Object : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject infoPanel;
    [SerializeField]
    VideoPlayer videoPlayer;

    Canvas canvas;
    RawImage videoImage;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
        videoImage = GameObject.Find("VideoImage").GetComponent<RawImage>();
    }

    public void SelectInfo()
    {
        infoPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void BackInfo()
    {
        infoPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
