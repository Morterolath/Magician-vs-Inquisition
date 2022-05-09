using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameStateController gameStateController;
    public bool isPanelOpen = false;
    public GameObject pauseMenuUI;
    public GameObject player;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPanelOpen)
            {
                ClosePanel();
            }
            else
            {
                OpenPanel();
            }
        }
    }
    public void ClosePanel()
    {
        player.GetComponent<Controller>().enabled = true;
        pauseMenuUI.SetActive(false);
        gameStateController.ResumeGame();
        gameStateController.isAnyPanelOpen = false;
        isPanelOpen = false;
    }
    public void OpenPanel()
    {
        if (!gameStateController.isAnyPanelOpen)
        {
            player.GetComponent<Controller>().enabled = false;
            pauseMenuUI.SetActive(true);
            gameStateController.PauseGame();
            gameStateController.isAnyPanelOpen = true;
            isPanelOpen = true;
        }
    }
    public void ResumeButton()
    {
        ClosePanel();
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
    public void RestartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    
}
