using UnityEngine;

public class HosePlaygroundController : MonoBehaviour
{
    [SerializeField] private Spawner _waterSpawner;
    [SerializeField] private Spawner[] _fireSpawners;

    private int _currentFireSpawnerIndex = 0;

    private void Start()
{
        _waterSpawner.OnSpawned += () =>
        {
            _waterSpawner.SetSpawning(false);
        };

        foreach (var fireSpawner in _fireSpawners)
        {

            fireSpawner.OnSpawned += () =>
            {
                _fireSpawners[_currentFireSpawnerIndex].SetSpawning(false);
                _waterSpawner.SetSpawning(true);
                _waterSpawner.SetDirection(_fireSpawners[_currentFireSpawnerIndex].transform.position - _waterSpawner.transform.position);

                _currentFireSpawnerIndex = (_currentFireSpawnerIndex + 1) % _fireSpawners.Length;
                _fireSpawners[_currentFireSpawnerIndex].SetSpawning(true);
            };
        }

        _fireSpawners[_currentFireSpawnerIndex].SetSpawning(true);
    }
}
