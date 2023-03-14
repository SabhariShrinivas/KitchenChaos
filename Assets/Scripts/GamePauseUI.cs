using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] Button mainMenuButton, resumeButton, optionsButton;
    private void Start()
    {
        GameStateManager.Instance.OnGamePaused += GameStateManager_OnGamePaused;
        GameStateManager.Instance.OnGameResumed += GameStateManager_OnGameResumed;
        Hide();

        mainMenuButton.onClick.AddListener(() => GoToMainMenu());
        resumeButton.onClick.AddListener(() => GameStateManager.Instance.TogglePauseGame());
        optionsButton.onClick.AddListener(() => OptionsUI.Instance.Show());
    }

    private void GameStateManager_OnGameResumed()
    {
        Hide();
    }

    private void GameStateManager_OnGamePaused()
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void GoToMainMenu()
    {
        Loader.LoadScene(Loader.Scene.MainMenu);
        Time.timeScale = 1;
    }
}
