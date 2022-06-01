using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ARdata
{
    public string ID;
    public string title;
    public string name;
    public string description;
    public VideoClip clip;
    public Sprite image;
    public string SceneName;
}
public class AR_Data : MonoBehaviour
{
    public static AR_Data instance;

    [SerializeField]
    Dictionary<string, ARdata> DataList;

    public Dictionary<string, ARdata> list { get => DataList; }


    private void Awake()
    {
        instance = this;
        DataList = Read("Museum_R_Data");
    }

    #region CSV
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public Dictionary<string, ARdata> Read(string file)
    {
        var list = new Dictionary<string, ARdata>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            ARdata tempdata = new ARdata();
            string number = int.Parse(values[0]).ToString("000");
            tempdata.ID = values[0];
            tempdata.title = values[1];
            tempdata.name = values[2];
            tempdata.description = values[3];
            tempdata.clip = Resources.Load<VideoClip>("Videos/" + number + "_Video");
            tempdata.image = Resources.Load<Sprite>("Images/Reference/Image_" + number);
            Debug.Log(tempdata.clip);
            if(values[4] == "1")
            {
                tempdata.SceneName = "Scenes/Scene_" + number;
            }
            else
            {
                tempdata.SceneName = "";
            }
            list.Add("Image_" + number, tempdata);
        }
        return list;
    }
    #endregion
}
