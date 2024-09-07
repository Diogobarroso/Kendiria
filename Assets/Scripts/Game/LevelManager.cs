using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _joinInstructions;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _waveCounter;
    // TODO: Fire "waves"?
    [SerializeField] private Fire _fire;

    private List<Character> _players = new List<Character>();
    [SerializeField] private List<Color> playerColors;

    [SerializeField] private List<Wave> waves;
    private int currentWave = 0;

    private void Start()
    {
        _playerManager.PlayerJoined += OnPlayerJoined;

        _waveCounter.text = 1 + "/" + waves.Count;
    }

    private void OnPlayerJoined(Character character)
    {
        _fire.gameObject.SetActive(true);
        _joinInstructions.SetActive(false);

        _players.Add(character);
        character._onDeath += OnPlayerDeath;

        character.SetColor(playerColors[_players.IndexOf(character)]);
    }

    private void OnPlayerDeath(Character character)
    {
        _players.Remove(character);

        if (_players.Count == 0)
        {
            _gameOverScreen.SetActive(true);
            _playerManager.DisableJoining();
        }
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    //Connect sth here? Maybe Fire?
    public void OnFireExtinguished()
    {
        _waveCounter.text = currentWave+1 + "/" + waves.Count;
        //Load new wave
    }
}
