using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _joinInstructions;
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
    }

    private void OnPlayerDeath(Character character)
    {
        _players.Remove(character);

        if (_players.Count == 0)
        {
            // TODO GAME OVER
            Debug.Log("Game Over");
        }

        character.SetColor(playerColors[_players.IndexOf(character)]);
    }
}
