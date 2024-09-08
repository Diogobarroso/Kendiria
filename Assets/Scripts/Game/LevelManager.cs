using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI _windSpeed;
    public Transform _windDirection;

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _joinInstructions;
    [SerializeField] private GameObject _startInstructions;
    [SerializeField] private CounterController _counterController;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameOverDefaultButton;
    [SerializeField] private TextMeshProUGUI _waveCounter;
    [SerializeField] private Fire _firePrefab;
    [SerializeField] private AudioSource _windAudioSource;
    private List<Character> _players = new List<Character>();
    [SerializeField] private List<Color> playerColors;

    [SerializeField] private List<Wave> waves;
    public int currentWave = -1;
    [SerializeField] private float _timeBetweenWaves = 5.0f;
    private float _startWaveTimer = 0.0f;
    public Fire _currentWaveFire = null;
    public Action<List<Transform>> OnWaveStart;

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
                foreach (var position in waves[currentWave].initialFlamesPosition)
                {
                    foreach (Character player in _players)
                    {
                        // TODO I don't have enough energy to make this magic values into configurable variables
                        if (Vector2.Distance(player.transform.position, position) < 1.0f)
                        {
                            Vector3 direction = (position - (Vector2) player.transform.position).normalized;
                            player.transform.position = player.transform.position + direction * 2.0f;
                        }
                    }
                }
                
                _currentWaveFire.gameObject.SetActive(true);
                _waveCounter.text = (currentWave + 1) + "/" + waves.Count;

                OnWaveStart?.Invoke(_currentWaveFire.flames);
            }
        }
    }

    private void OnPlayerJoined(Character character)
    {
        _joinInstructions.SetActive(false);
        _startInstructions.SetActive(true);

        _players.Add(character);
        character.OnReady += OnPlayerReady;
        character._onDeath += OnPlayerDeath;

        character.SetColor(playerColors[_players.IndexOf(character)]);
    }

    private void OnPlayerReady()
    {
        if (currentWave == -1) {
            _startInstructions.SetActive(false);
            StartNextWave();
            _playerManager.DisableJoining();
        }
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
        _currentWaveFire.SetWind(waves[currentWave].windAngle, waves[currentWave].windSpeed);
        _currentWaveFire.SetFlameSpreadSpeed(waves[currentWave].flameSpreadSpeed);

        foreach (var position in waves[currentWave].initialFlamesPosition)
        {
            _currentWaveFire.AddFlame(position);
        }
        if (waves[currentWave].windSpeed > 0.0f)
        {
            _windSpeed.text = waves[currentWave].windSpeed + " m/s";
            _windDirection.rotation = Quaternion.Euler(0, 0, waves[currentWave].windAngle);
            
        }
        else
        {
            _windSpeed.text = "No wind";
            _windDirection.rotation = Quaternion.Euler(0, 0, 0);
        }
        _windAudioSource.volume = Mathf.Clamp01(0.4f * waves[currentWave].windSpeed);
        _windAudioSource.panStereo = Mathf.Sin(waves[currentWave].windAngle*Mathf.PI/180);
        _startWaveTimer = _timeBetweenWaves;
        _counterController.AnimateCounter();
    }

    private void OnPlayerDeath(Character character)
    {
        _players.Remove(character);

        if (_players.Count == 0)
        {
            _gameOverScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_gameOverDefaultButton);
            //_playerManager.DisableJoining();
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
            //_playerManager.DisableJoining();
            EventSystem.current.SetSelectedGameObject(_gameOverDefaultButton);
        }
        else
        {
            StartNextWave();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {

    }
}
