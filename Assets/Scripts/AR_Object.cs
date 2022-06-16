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
    GameObject SceneLoadButton;
    string scenename;
    public ARdata arData { get; private set; }

    public bool isSetData { get; private set; } = false;
    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    public void SetData(ARdata data)
    {

        titleText.text = data.title + "\n- " + data.name;
        isSetData = true;
        arData = data;
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

}
