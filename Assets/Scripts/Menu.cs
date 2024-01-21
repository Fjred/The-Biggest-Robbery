using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button playButton;
    public Button infoButton;
    public Button exitButton;
    public Button backButton;

    public GameObject menuPanel;
    public GameObject informationPanel;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playButton.onClick.AddListener(Play);
        //exitButton.onClick.AddListener(Exit);
        //infoButton.onClick.AddListener(Info);
        //backButton.onClick.AddListener(Back);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Info()
    {
        menuPanel.SetActive(false);
        informationPanel.SetActive(true);
    }
    public void Back()
    {
        menuPanel.SetActive(true);
        informationPanel.SetActive(false);
    }
}
