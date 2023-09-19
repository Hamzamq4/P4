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
    public GameObject startPanel;
    public Button start, fremskridt, indstillinger, traffik, skole;

    void Start()
    {
        start.onClick.AddListener(delegate { ChangePanel("start"); });
        fremskridt.onClick.AddListener(delegate { ChangePanel("fremskridt"); });
        indstillinger.onClick.AddListener(delegate { ChangePanel("indstillinger"); });  
        traffik.onClick.AddListener(delegate { ChangeScene("traffik");});
        skole.onClick.AddListener(delegate { ChangeScene("skole");});
    }

    public void DisablePanels()
    {
        mainMenuPanel.SetActive(false);
        fremskridtPanel.SetActive(false);
        indstillingerPanel.SetActive(false);
        startPanel.SetActive(false);
    }

    public void ChangePanel(string panelName)
    {
        switch (panelName)
        {
            case "start":
                DisablePanels();
                /*Time.timeScale = 1;
                ChangeScene("SampleScene");*/
                startPanel.SetActive(true);
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
        Debug.Log("Change scene");
        if (sceneName == "traffik")
        {
            Debug.Log("Traffik");
            SceneManager.LoadScene("TraffikScene");
        }
        else if (sceneName == "skole")
        {
            SceneManager.LoadScene("SkoleScene");
        }
    }
}
