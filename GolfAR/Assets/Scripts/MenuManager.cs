using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject selectMenu;
    public GameObject singlePlayerMenu;
    public GameObject multiPlayerMenu;

    private void Start()
    {
        selectMenu.SetActive(true);
        singlePlayerMenu.SetActive(false);
        multiPlayerMenu.SetActive(false);
    }

    public void ClickSingleplayer(){
        selectMenu.SetActive(false);
        singlePlayerMenu.SetActive(true);
    }
}
