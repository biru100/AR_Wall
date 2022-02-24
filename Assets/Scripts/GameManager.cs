using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject videoPanel;
    [SerializeField]
    RawImage videoImage;
    [SerializeField]
    GameObject imagePanel;
    [SerializeField]
    GameObject ExitPanel;
    [SerializeField]
    Image resource2D;
    

    public RawImage videoimage { get => videoImage; }
    public Image resource2d { get => resource2D; }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(!ExitPanel.activeSelf);
        }
    }

    public void Exit(bool check)
    {
        if(check)
        {
            Application.Quit();
        }
        else
        {
            ExitPanel.SetActive(false);
        }
    }

    public void SelectVideo(bool check)
    {
        videoPanel.SetActive(check);
    }

    public void SelectImage(bool check)
    {
        imagePanel.SetActive(check);
    }
}
