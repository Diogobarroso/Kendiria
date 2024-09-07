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

    private void Start()
    {
        _playerManager.PlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(Character character)
    {
        _fire.gameObject.SetActive(true);
        _joinInstructions.SetActive(false);

        _players.Add(character);
    }
}
