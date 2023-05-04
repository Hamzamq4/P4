using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

[System.Serializable]
public class GameMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject scoreUI;
    public GameObject fromMenu;

    public Button pause, genoptag, pauseAfslut, tryAgain, gameOverAfslut;

    // Start is called before the first frame update
    void Start()
    {
        pause.onClick.AddListener(delegate { ChangePanel("pause"); });
        genoptag.onClick.AddListener(delegate { ChangePanel("genoptag"); });
        pauseAfslut.onClick.AddListener(delegate { ChangePanel("pauseAfslut"); });
        tryAgain.onClick.AddListener(delegate { ChangePanel("tryAgain"); });
        gameOverAfslut.onClick.AddListener(delegate { ChangePanel("tryAgainAfslut"); });
        fromMenu = GameObject.FindGameObjectWithTag("FromMenu");

    }

    public void DisablePanels()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ChangePanel(string panelName)
    {
        switch (panelName)
        {
            case "pause":
                Debug.Log("Pause");
                DisablePanels();
                pause.gameObject.SetActive(false);
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                break;
            case "genoptag":
                Debug.Log("Genoptag");
                DisablePanels();
                pause.gameObject.SetActive(true);
                Time.timeScale = 1;
                break;
            case "pauseAfslut":
                Debug.Log("PauseAfslut");
                DisablePanels();
                SceneManager.LoadScene("MenuScene");
                Time.timeScale = 1;
                scoreUI.gameObject.SetActive(true);
                ScoreManager.health = 2;
                break;
            case "tryAgain":
                Debug.Log("Prøv igen");
                Destroy(fromMenu);
                DisablePanels();
                Time.timeScale = 1;
                SceneManager.LoadScene("SampleScene");
                scoreUI.gameObject.SetActive(true);
                ScoreManager.health = 2;
                ScoreManager.isPlayerAlive = true;
                break;
            case "tryAgainAfslut":
                Debug.Log("TryAgainAfslut");
                DisablePanels();
                Time.timeScale = 1;
                SceneManager.LoadScene("MenuScene");
                scoreUI.gameObject.SetActive(true);
                ScoreManager.health = 2;
                break;
        }
    }
}
