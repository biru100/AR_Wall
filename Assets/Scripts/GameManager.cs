using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ManagerUI
{
    public GameObject TitlePanel;
    public GameObject MainPanel;
    public GameObject VideoPanel;
    public RectTransform InfoPanel;
    public GameObject imagePanel;
    public GameObject ExitPanel;
    public GameObject MenuPanel;
    public GameObject ARFindPanel;
    public GameObject Scene3DButton;

    public RectTransform SwipeArrow;

    public Animator InfoAnim;
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
    [SerializeField]
    Image PanelImage;

    AR_Object trackedObject;

    Image[] ImageList;

    Vector2 ImageSizetemp;
    Vector2 PanelImageSizetemp;

    public VideoPlayer player { get; private set; }

    public Image resource2d { get => resource2D; }
    public Camera arCamera { get => ARCamera; }

    bool infocheck = false;
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
        ImageList = new Image[10];
        ImageList[0] = PanelImage;
        for(int i = 1; i < 10; i++)
        {
            ImageList[i] = Instantiate(PanelImage).GetComponent<Image>();
            ImageList[i].transform.parent = ImageList[0].transform.parent;
            //ImageList[i].transform.position = ImageList[0].transform.position + Vector3.right * ImageList[0].rectTransform.rect.width * i;
        }
        managerUI.InfoPanel.gameObject.SetActive(false);
        managerUI.MenuPanel.SetActive(false);
        managerUI.ARFindPanel.SetActive(true);
        player = GetComponent<VideoPlayer>();
        player.targetCamera = VideoCamera;
        ImageSizetemp = ObjectImage.rectTransform.sizeDelta;
        PanelImageSizetemp = PanelImage.rectTransform.sizeDelta;
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
    }

    void TrackedImage(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            trackedObject = newImage.gameObject.GetComponent<AR_Object>();

            for (int i = 0; i < AR_Data.instance.list.Length; i++)
            {
                if (newImage.referenceImage.name == AR_Data.instance.list[i].ID)
                {
                    trackedObject.SetData(AR_Data.instance.list[i]);
                    managerUI.Scene3DButton.SetActive(trackedObject.arData.SceneName != "");
                    SetData(AR_Data.instance.list[i]);
                    managerUI.InfoPanel.gameObject.SetActive(true);
                    managerUI.MenuPanel.SetActive(true);
                    managerUI.ARFindPanel.SetActive(false);
                    break;
                }
            }
        }
        bool check = false;
        foreach (var updateImage in eventArgs.updated)
        {
            switch (updateImage.trackingState)
            {
                case TrackingState.None:
                    updateImage.gameObject.SetActive(false);
                    break;
                case TrackingState.Limited:
                    updateImage.gameObject.SetActive(false);
                    break;
                case TrackingState.Tracking:
                    updateImage.gameObject.SetActive(true);
                    check = true;
                    if (trackedObject.gameObject != updateImage.gameObject)
                    {
                        trackedObject = updateImage.gameObject.GetComponent<AR_Object>();
                        managerUI.Scene3DButton.SetActive(trackedObject.arData.SceneName != "");
                        SetData(trackedObject.arData);
                    }
                    break;
            }
        }
        managerUI.InfoPanel.gameObject.SetActive(check);
        managerUI.MenuPanel.SetActive(check);
        managerUI.ARFindPanel.SetActive(!check);
        foreach (var removeImage in eventArgs.removed)
        {
            removeImage.gameObject.SetActive(false);

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
        ObjectImage.sprite = data.image[0];
        PanelImage.sprite = data.image[0];
        SetImagePixel(ObjectImage, ImageSizetemp.x, ImageSizetemp.y);
        SetImagePixel(PanelImage, PanelImageSizetemp.x, PanelImageSizetemp.y);
    }

    void SetImagePixel(Image image, float x, float y)
    {
        float temp;
        float texutreWidth = image.sprite.texture.width;
        float textureHeight = image.sprite.texture.height;
        if (texutreWidth / textureHeight > x / y)
        {
            temp = x / texutreWidth * textureHeight;
            image.rectTransform.sizeDelta = new Vector2(x, temp);
        }
        else
        {
            temp = y / textureHeight * texutreWidth;
            image.rectTransform.sizeDelta = new Vector2(temp, y);
        }
    }

    public void Go3DScene()
    {
        if(trackedObject.arData.SceneName != "")
        {
            SceneManager.LoadScene(trackedObject.arData.SceneName);
        }
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
    }

    void ViewInfo(bool check)
    {
        if (check)
        {
            managerUI.InfoAnim.SetTrigger("View");
        }
        else
        {
            managerUI.InfoAnim.SetTrigger("Hide");
        }
    }
}

