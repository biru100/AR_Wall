using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

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
    [SerializeField]
    ARTrackedImageManager imageManager;

    AR_Object trackedObject;

    bool activeCheck;

    public RawImage videoimage { get => videoImage; }
    public Image resource2d { get => resource2D; }

    private void Awake()
    {
        instance = this;
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
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            updatedImage.gameObject.SetActive(updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);
            if(updatedImage.gameObject.activeSelf)
            {
                if(!activeCheck)
                {
                    activeCheck = true;
                    for(int i = 0; i < AR_Data.instance.list.Length; i++)
                    {
                        if (updatedImage.referenceImage.name == AR_Data.instance.list[i].ID)
                        {
                            trackedObject.SetData(AR_Data.instance.list[i]);
                            break;
                        }
                    }
                }
            }
            else
            {
                activeCheck = false;
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
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
