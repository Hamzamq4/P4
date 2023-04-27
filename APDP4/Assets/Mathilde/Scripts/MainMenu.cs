using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject fremskridtPanel;
    public GameObject indstillingerPanel;

    public Button start, fremskridt, indstillinger;

    void Start()
    {
        start.onClick.AddListener(delegate { ChangePanel("start"); });
        fremskridt.onClick.AddListener(delegate { ChangePanel("fremskridt"); });
        indstillinger.onClick.AddListener(delegate { ChangePanel("indstillinger"); });  
    }

    public void DisablePanels()
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
