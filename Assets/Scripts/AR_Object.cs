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
    [SerializeField]
    Button videoButton;
    [SerializeField]
    Button imageButton;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Sprite sprite2d;
    RawImage videoImage;
    Image image2d;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
        videoImage = GameManager.instance.videoimage;
        image2d = GameManager.instance.resource2d;
        image2d.sprite = sprite2d;
        videoImage.texture = videoPlayer.targetTexture;
        videoButton.onClick.AddListener(videoOnClick);
        imageButton.onClick.AddListener(imageOnClick);
    }

    public void SelectInfo(bool check)
    {
        infoPanel.SetActive(check);
        menuPanel.SetActive(!check);
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
