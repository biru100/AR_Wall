using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject infoPanel;


    public void SelectInfo()
    {
        infoPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void BackInfo()
    {
        infoPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
