using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Action OnSpawned;

    [SerializeField] private Rigidbody2D _prefab;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private Transform _spawnHolder;

    private float _timer;
    private bool _isSpawning = false;

    // TODO Bezier curves?
    [SerializeField] private float _spawnedObjectsSpeed = 1f;
    [SerializeField] private Vector2 _spawnedObjectsDirection = Vector2.right;

    public void SetSpawning(bool spawning)
    {
        if (_isSpawning == spawning) return;

        _isSpawning = spawning;

        if (spawning) _timer = _spawnRate;
    }

    public void SetDirection(Vector2 direction)
    {
        _spawnedObjectsDirection = direction.normalized;
    }

    private void Update()
    {
        if (!_isSpawning) return;

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _spawnRate;
            Spawn();
        }
    }

    private void Spawn()
    {
        Rigidbody2D instance = Instantiate(_prefab, _spawnHolder.position, Quaternion.identity);
        instance.transform.SetParent(_spawnHolder);
        instance.velocity = _spawnedObjectsSpeed * _spawnedObjectsDirection;

        OnSpawned?.Invoke();
    }
}
