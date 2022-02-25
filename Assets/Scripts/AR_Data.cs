using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class ARdata
{
    public string ID;
    public string title;
    public string name;
    public string description;
    public VideoClip clip;
    public Sprite image;
}
public class AR_Data : MonoBehaviour
{
    public static AR_Data instance;

    [SerializeField]
    ARdata[] List;

    public ARdata[] list { get => List; }


    private void Awake()
    {
        instance = this; 
    }
}
