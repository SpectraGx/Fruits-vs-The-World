using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuMobile : MonoBehaviour
{
    private Dictionary<GameObject, GameObject> panelFirstButtonMapping;
    private int controlsNum;
    private bool taptostart = false;

    [Header("Inspector: Panels")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject controlsKeyboardMenu;
    [SerializeField] private GameObject controlsControlsMenu;
    [SerializeField] private GameObject controlsHow;
    [SerializeField] private GameObject creditsMenu;


    [Header("Inspecor: FirstButton")]
    [SerializeField] private GameObject mainFirstButton;
    [SerializeField] private GameObject optionsFirstButton;
    [SerializeField] private GameObject controlsFirstButton;
    [SerializeField] private GameObject creditsFirstButton;


    private void Awake()
    {
        titlePanel.SetActive(true);
        panelFirstButtonMapping = new Dictionary<GameObject, GameObject>(){
            {mainMenu, mainFirstButton},
            {optionsMenu, optionsFirstButton},
            {controlsMenu, controlsFirstButton},
            {creditsMenu, creditsFirstButton},
        };
        taptostart = false;
    }

    private void Update()
    {
        if (taptostart == false)
        {
            if (Input.anyKeyDown)
            {
                OpenMainMenu();
            }
        }
    }

    public void Play(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        foreach (var panel in panelFirstButtonMapping.Keys)
        {
            panel.SetActive(false);
        }

        panelToOpen.SetActive(true);

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (panelFirstButtonMapping.TryGetValue(panelToOpen, out GameObject firstButton))
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
    }

    public void OpenMainMenu()
    {
        OpenPanel(mainMenu);
        taptostart = true;
        titlePanel.SetActive(false);
    }

    public void OpenOptions()
    {
        OpenPanel(optionsMenu);
    }

    public void OpenControls()
    {
        OpenPanel(controlsMenu);
    }

    public void NextControl()
    {
        ControlsPanel();
    }

    public void BackControl()
    {
        ControlsPanel();
    }

    public void ControlsPanel()
    {

        controlsNum = Mathf.Clamp(controlsNum, 0, 2);
        switch (controlsNum)
        {
            case 0:
                controlsKeyboardMenu.SetActive(false);
                controlsHow.SetActive(false);
                controlsControlsMenu.SetActive(true);
                break;
            case 1:
                controlsKeyboardMenu.SetActive(false);
                controlsHow.SetActive(true);
                controlsControlsMenu.SetActive(false);
                break;
            case 2:
                controlsKeyboardMenu.SetActive(true);
                controlsHow.SetActive(false);
                controlsControlsMenu.SetActive(false);
                break;
        }
    }

    public void OpenCredits()
    {
        OpenPanel(creditsMenu);
    }

    public void URL(string url)
    {
        Application.OpenURL(url);
    }

}
