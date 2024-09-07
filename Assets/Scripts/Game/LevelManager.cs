using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _joinInstructions;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _waveCounter;
    [SerializeField] private Fire _firePrefab;

    private List<Character> _players = new List<Character>();
    [SerializeField] private List<Color> playerColors;

    [SerializeField] private List<Wave> waves;
    private int currentWave = -1;
    [SerializeField] private float _timeBetweenWaves = 5.0f;
    private float _startWaveTimer = 0.0f;
    private Fire _currentWaveFire = null;

    private void Start()
    {
        _playerManager.PlayerJoined += OnPlayerJoined;

        _waveCounter.text = 1 + "/" + waves.Count;
    }

    private void Update()
    {
        if (_startWaveTimer > 0.0f)
        {
            _startWaveTimer -= Time.deltaTime;

            if (_startWaveTimer <= 0.0f)
            {
                _currentWaveFire.gameObject.SetActive(true);
                _waveCounter.text = (currentWave + 1) + "/" + waves.Count;
            }
        }
    }

    private void OnPlayerJoined(Character character)
    {
        _joinInstructions.SetActive(false);

        _players.Add(character);
        character.OnReady += OnPlayerReady;
        character._onDeath += OnPlayerDeath;

        character.SetColor(playerColors[_players.IndexOf(character)]);
    }

    private void OnPlayerReady()
    {
        if (currentWave == -1) StartNextWave();
    }

    private void StartNextWave()
    {
        if (_currentWaveFire != null &&_currentWaveFire.flames.Count > 0) return;

        currentWave++;

        if (_currentWaveFire != null)
            Destroy(_currentWaveFire.gameObject);
        
        _currentWaveFire = Instantiate(_firePrefab, Vector2.zero, Quaternion.identity);
        _currentWaveFire.gameObject.SetActive(false);
        _currentWaveFire.OnFireExtinguished += OnFireExtinguished;

        foreach (var position in waves[currentWave].initialFlamesPosition)
        {
            _currentWaveFire.AddFlame(position);
        }

        _startWaveTimer = _timeBetweenWaves;
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
    
    public void OnFireExtinguished()
    {
        if (currentWave == waves.Count - 1)
        {
            _gameOverScreen.SetActive(true);
            _playerManager.DisableJoining();
        }
        else
        {
            StartNextWave();
        }
    }
}
