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

    [SerializeField]
    Text descriptionText;

    [SerializeField]
    GameObject videoImage;

    VideoPlayer player;
    private void Start()
    {
        canvas.worldCamera = Camera.main;
        player = GameManager.instance.player;
        player.targetCamera = GameManager.instance.arCamera;
    }

    public void SetData(ARdata data)
    {
        if(player == null)
        {
            player = GameManager.instance.player;
        }

        titleText.text = data.title + "\n- " + data.name;
        descriptionText.text = data.description;
        if(data.clip != null)
        {
            GameManager.instance.player.clip = data.clip;
        }
        else
        {
            videoImage.SetActive(false);
        }
    }
    public void ChangeVideoMode(bool check)
    {
        GameManager.instance.ChangeVideoMode(check);
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
