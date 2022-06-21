using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject controlsScreen;

    public Button playButton;
    public Button controlsButton;
    public Button exitControlsButton;
    public Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => { StartGame(); });
        controlsButton.onClick.AddListener(() => { ActivateControlsScreen(); });
        quitButton.onClick.AddListener(() => { QuitGame(); });
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ActivateControlsScreen()
    {
        controlsScreen.SetActive(true);
        exitControlsButton.onClick.AddListener(() => { TurnOffControlsScreen(); });
    }

    private void TurnOffControlsScreen()
    {
        controlsScreen.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
