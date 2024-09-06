using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _credits;

    void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _creditsButton.onClick.AddListener(ShowCredits);
        _quitButton.onClick.AddListener(QuitGame);
        _backButton.onClick.AddListener(ShowMainMenu);
    }

    private void ShowMainMenu()
    {
        _credits.SetActive(false);
        _mainMenu.SetActive(true);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void ShowCredits()
    {
        _credits.SetActive(true);
        _mainMenu.SetActive(false);
    }

    private void StartGame()
    {
        throw new NotImplementedException();
    }
}
