using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    Image ObjectImage;

    [SerializeField]
    GameObject SceneLoadButton;

    VideoPlayer player;
    string scenename;
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
        //descriptionText.text = data.description;
        //ObjectImage.sprite = data.image;
        //scenename = data.SceneName;
        //SceneLoadButton.SetActive(scenename != "");
        GameManager.instance.SetData(data);
        if(data.clip != null)
        {
            GameManager.instance.player.clip = data.clip;
        }
        else
        {
            ObjectImage.gameObject.SetActive(true);
            videoImage.SetActive(false);
            data.clip = null;
        }
    }
    public void ChangeVideoMode(bool check)
    {
        GameManager.instance.ChangeVideoMode(check);
    }

    public void Go3DScene()
    {
        SceneManager.LoadScene(scenename);
    }

    public void SelectInfo()
    {
        GameManager.instance.SelectInfo();
    }

    void videoOnClick()
    {
        GameManager.instance.SelectVideo(true);
    }

    void imageOnClick()
    {
        GameManager.instance.SelectImage();
    }
    
}
