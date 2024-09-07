using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _joinInstructions;
    [SerializeField] private GameObject _gameOverScreen;
    // TODO: Fire "waves"?
    [SerializeField] private Fire _fire;

    private List<Character> _players = new List<Character>();
    [SerializeField] private List<Color> playerColors;

    private void Start()
    {
        _playerManager.PlayerJoined += OnPlayerJoined;
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
            Debug.Log("Game Over");
        }
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
