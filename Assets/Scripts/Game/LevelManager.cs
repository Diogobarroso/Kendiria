using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _joinInstructions;
    // TODO: Fire "waves"?
    [SerializeField] private Fire _fire;

    private void Start()
    {
        _playerManager.PlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined()
    {
        _fire.gameObject.SetActive(true);
        _joinInstructions.SetActive(false);
    }
}
