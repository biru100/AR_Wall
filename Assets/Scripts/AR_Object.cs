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
        player = GetComponent<VideoPlayer>();
    }

    public void SetData(ARdata data)
    {
        titleText.text = data.title + "\n- " + name;
        descriptionText.text = data.description;
        if(data.clip != null)
        {
            videoImage.SetActive(true);
            player.clip = data.clip;
        }
        else
        {
            videoImage.SetActive(false);
        }
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
