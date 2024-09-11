using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _controlsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _controlsBackButton;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _controls;

    [SerializeField] private string _gameSceneName = "MainGameScene";

    void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _creditsButton.onClick.AddListener(ShowCredits);
        _quitButton.onClick.AddListener(QuitGame);
        _backButton.onClick.AddListener(ShowMainMenu);
        _controlsButton.onClick.AddListener(ShowControls);
        _controlsBackButton.onClick.AddListener(ShowMainMenu);

#if UNITY_WEBGL
        _quitButton.gameObject.SetActive(false);
#endif
    }

    private void ShowMainMenu()
    {
        _credits.SetActive(false);
        _controls.SetActive(false);
        _mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_startButton.gameObject);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void ShowCredits()
    {
        _credits.SetActive(true);
        _controls.SetActive(false);
        _mainMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_backButton.gameObject);
    }

    private void ShowControls()
    {
        _credits.SetActive(false);
        _controls.SetActive(true);
        _mainMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_controlsBackButton.gameObject);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
}
