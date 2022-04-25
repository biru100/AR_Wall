using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

[System.Serializable]
public class ManagerUI
{
    public GameObject TitlePanel;
    public GameObject MainPanel;
    public GameObject VideoPanel;
    public RectTransform InfoPanel;
    public GameObject imagePanel;
    public GameObject ExitPanel;

    public Slider VideoPortraitSlider;
    public Slider VideoLandScapeSlider;

    public GameObject VideoPlayButton;
    public GameObject VideoPauseButton;

    public RectTransform SwipeArrow;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    Camera ARCamera;
    [SerializeField]
    Camera VideoCamera;
    [SerializeField]
    ManagerUI managerUI;
    [SerializeField]
    Image resource2D;
    [SerializeField]
    ARTrackedImageManager imageManager;
    [SerializeField]
    Text titleText;
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    Image ObjectImage;

    AR_Object trackedObject;

    public VideoPlayer player { get; private set; }

    public Image resource2d { get => resource2D; }
    public Camera arCamera { get => ARCamera; }

    float detailValue = 0;

    bool infocheck = false;

    Vector2 targetPos;
    Vector3 swipeTargetRot;
    Vector3 swipeTargetPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Invoke("DisableTitle", 2.3f);
        }
        else
        {
            instance = this;
            DisableTitle();
        }
        player = GetComponent<VideoPlayer>();
        player.targetCamera = VideoCamera;
        ChangeVideoMode(false);
        ViewInfo(false);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (VideoCamera.gameObject.activeSelf)
            {
                ChangeVideoMode(false);
            }
            else
            {
                managerUI.ExitPanel.SetActive(!managerUI.ExitPanel.activeSelf);
            }
        }
        //managerUI.VideoLandScapeSlider.gameObject.SetActive(Screen.orientation == ScreenOrientation.Landscape);
        //managerUI.VideoPortraitSlider.gameObject.SetActive(Screen.orientation != ScreenOrientation.Landscape);
        if (managerUI.InfoPanel.anchoredPosition != targetPos)
        {
            managerUI.InfoPanel.anchoredPosition = Vector3.Lerp(managerUI.InfoPanel.anchoredPosition, targetPos, 0.1f);
            managerUI.SwipeArrow.transform.eulerAngles = Vector3.Lerp(managerUI.SwipeArrow.transform.eulerAngles, swipeTargetRot, 0.1f);
            managerUI.SwipeArrow.anchoredPosition = Vector3.Lerp(managerUI.SwipeArrow.anchoredPosition, swipeTargetPos, 0.1f);
        }
    }

    void TrackedImage(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
            trackedObject = newImage.gameObject.GetComponent<AR_Object>();
            trackedObject.gameObject.SetActive(true);
            for (int i = 0; i < AR_Data.instance.list.Length; i++)
            {
                if (newImage.referenceImage.name == AR_Data.instance.list[i].ID)
                {
                    trackedObject.SetData(AR_Data.instance.list[i]);
                    break;
                }
            }
        }
        foreach (var newImage in eventArgs.removed)
        {
            newImage.gameObject.SetActive(false);
            player.clip = null;
            player.targetTexture.Release();
        }
    }

    void DisableTitle()
    {
        managerUI.TitlePanel.SetActive(false);
    }

    public void SetData(ARdata data)
    {
        titleText.text = data.title + "\n- " + data.name;
        descriptionText.text = data.description;
        ObjectImage.sprite = data.image;
    }

    public void ChangeVideoMode(bool check)
    {
        VideoCamera.gameObject.SetActive(check);
        arCamera.gameObject.transform.root.gameObject.SetActive(!check);
        managerUI.MainPanel.SetActive(!check);
        managerUI.VideoPanel.SetActive(check);

        if (check)
        {
            player.renderMode = VideoRenderMode.CameraNearPlane;
            player.targetCamera = VideoCamera;
            player.audioOutputMode = VideoAudioOutputMode.Direct;
        }
        else
        {

            player.renderMode = VideoRenderMode.RenderTexture;
            player.audioOutputMode = VideoAudioOutputMode.None;
        }

    }
    public void Exit(bool check)
    {
        if (check)
        {
            Application.Quit();
        }
        else
        {
            managerUI.ExitPanel.SetActive(false);
        }
    }

    public void SelectInfo()
    {
        ViewInfo(!infocheck);
        infocheck = !infocheck;
    }

    public void SelectVideo(bool check)
    {
        arCamera.gameObject.SetActive(!check);
    }

    public void SelectImage()
    {
        managerUI.imagePanel.SetActive(!managerUI.imagePanel.activeSelf);
    }
    public void VideoPlay(bool check)
    {
        if (check)
        {
            player.Play();
        }
        else
        {
            player.Pause();
        }

        managerUI.VideoPlayButton.SetActive(player.isPlaying);
        managerUI.VideoPauseButton.SetActive(player.isPaused);
    }

    void ViewInfo(bool check)
    {
        Vector3 infotemp = managerUI.InfoPanel.transform.position;
        if (check)
        {
            float y = -750;
            targetPos = new Vector3(0, y);
            swipeTargetRot = new Vector3(0, 0, 45);
            managerUI.InfoPanel.anchorMax = new Vector2(1, 0);
            managerUI.InfoPanel.anchorMin = new Vector2(0, 0);
            swipeTargetPos = new Vector3(0, -80, 0);
        }
        else
        {
            float y = -1158;
            targetPos = new Vector3(0, y);
            swipeTargetRot = new Vector3(0, 0, 225);
            managerUI.InfoPanel.anchorMax = new Vector2(1, 1);
            managerUI.InfoPanel.anchorMin = new Vector2(0, 1);
            swipeTargetPos = new Vector3(0, -200, 0);
        }
        managerUI.InfoPanel.transform.position = infotemp;
    }
}

