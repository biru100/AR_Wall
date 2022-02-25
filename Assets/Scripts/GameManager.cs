using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    Camera ARCamera;
    [SerializeField]
    Camera VideoCamera;
    [SerializeField]
    RawImage videoImage;
    [SerializeField]
    GameObject imagePanel;
    [SerializeField]
    GameObject ExitPanel;
    [SerializeField]
    Image resource2D;
    [SerializeField]
    ARTrackedImageManager imageManager;

    AR_Object trackedObject;

    public VideoPlayer player { get; private set; }

    public RawImage videoimage { get => videoImage; }
    public Image resource2d { get => resource2D; }
    public Camera arCamera { get => ARCamera; }

    private void Awake()
    {
        instance = this;
        player = GetComponent<VideoPlayer>();
        player.targetCamera = VideoCamera;
        ChangeVideoMode(false);
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += TrackedImage;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= TrackedImage;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(!ExitPanel.activeSelf);
        }
    }

    void TrackedImage(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
            trackedObject = newImage.gameObject.GetComponent<AR_Object>();
            for (int i = 0; i < AR_Data.instance.list.Length; i++)
            {
                if (newImage.referenceImage.name == AR_Data.instance.list[i].ID)
                {
                    trackedObject.SetData(AR_Data.instance.list[i]);
                    break;
                }
            }
        }
    }
    public void ChangeVideoMode(bool check)
    {
        if (check)
        {
            VideoCamera.gameObject.SetActive(true);
            arCamera.gameObject.transform.root.gameObject.SetActive(false);
            player.renderMode = VideoRenderMode.CameraNearPlane;
            player.targetCamera = VideoCamera;
            player.audioOutputMode = VideoAudioOutputMode.Direct;
        }
        else
        {
            VideoCamera.gameObject.SetActive(false);
            arCamera.gameObject.transform.root.gameObject.SetActive(true);
            player.renderMode = VideoRenderMode.RenderTexture;
            player.audioOutputMode = VideoAudioOutputMode.None;
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
        arCamera.gameObject.SetActive(!check);
    }

    public void SelectImage(bool check)
    {
        imagePanel.SetActive(check);
    }
}
