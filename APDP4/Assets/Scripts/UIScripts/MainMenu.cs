using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script serves as the main functionality for the initial screen upon opening the game. 
/// </summary>
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject fremskridtPanel;
    public GameObject indstillingerPanel;

    public Button start, fremskridt, indstillinger;

    void Start() // Adds listeners that detect when a click event occurs
    {
        start.onClick.AddListener(delegate { ChangePanel("start"); });
        fremskridt.onClick.AddListener(delegate { ChangePanel("fremskridt"); });
        indstillinger.onClick.AddListener(delegate { ChangePanel("indstillinger"); });  
    }

    public void DisablePanels() // Adds listeners that detect when a click event occurs

    {
        mainMenuPanel.SetActive(false);
        fremskridtPanel.SetActive(false);
        indstillingerPanel.SetActive(false);
    }

    public void ChangePanel(string panelName)
    {
        switch (panelName)
        {
            case "start":
                DisablePanels();
                Time.timeScale = 1; //Sets timescale to normal speed
                ChangeScene("SampleScene");
                break;
            case "fremskridt":
                DisablePanels();
                fremskridtPanel.SetActive(true); 
                break;
            case "indstillinger":
                DisablePanels();
                indstillingerPanel.SetActive(true);
                break;
        }
    }

    public void BackButton()
    {
        DisablePanels();
        mainMenuPanel.SetActive(true);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
