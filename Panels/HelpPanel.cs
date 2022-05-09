using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    public GameStateController gameStateController;
    public bool gameIsPaused = true;
    public GameObject helpMenuUI;
    public GameObject player;
    private void Start()
    {
        OpenPanel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (gameIsPaused)
            {
                ClosePanel();
            }
            else
            {
                OpenPanel();
            }
        }
    }
    void ClosePanel()
    {
        player.GetComponent<Controller>().enabled = true;
        helpMenuUI.SetActive(false);
        gameStateController.ResumeGame();
        gameStateController.isAnyPanelOpen = false;
        gameIsPaused = false;
    }
    void OpenPanel()
    {
        if (!gameStateController.isAnyPanelOpen)
        {
            player.GetComponent<Controller>().enabled = false;
            helpMenuUI.SetActive(true);
            gameStateController.PauseGame();
            gameStateController.isAnyPanelOpen = true;
            gameIsPaused = true;
        }

    }
}
