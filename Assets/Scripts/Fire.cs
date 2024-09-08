using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private FlameController flamePrefab;
    [SerializeField] private float flameSpreadSpeed; 
    [SerializeField] private float flameSpreadDistance;
    [SerializeField] private float _treeSpreadAcceleration;
    [SerializeField] private float flameAnimSpeed;
    [SerializeField] private float flameAnimHeight;
    public float windAngle;
    public float windSpeed;
    [SerializeField] private AudioClip flameExtinguishSFX;
    [HideInInspector] public List<Transform> flames = new List<Transform>();
    public Action OnFireExtinguished;

    private void Start()
    {
        //AddFlame(transform.position);
    }

    public bool TryExtinguish(Transform flame)
    {
        int index = flames.FindIndex(a => a != null && a.transform == flame);
        if (index == -1)
            return false;
        flames.RemoveAt(index);
        AudioSource.PlayClipAtPoint(flameExtinguishSFX, flame.position, 1);

        if (flames.Count == 0)
        {
            OnFireExtinguished?.Invoke();
        }
        return true;
    }

    public void AddFlame(Vector3 position)
    {
        FlameController flameController = Instantiate(flamePrefab, position, Quaternion.identity, this.transform);
        flameController.Initialize(this);
        flames.Add(flameController.transform);
    }

    public float GetFlameSpreadSpeed()
    {
        return flameSpreadSpeed;
    }

    public void SetFlameSpreadSpeed(float newSpreadSpeed)
    {
        flameSpreadSpeed = newSpreadSpeed;
    }

    public float GetFlameSpreadDistance()
    {
        return flameSpreadDistance;
    }

    public float GetFlameAnimHeight()
    {
        return flameAnimHeight;
    }

    public float GetFlameAnimSpeed()
    {
        return flameAnimSpeed;
    }

    public float GetTreeSpreadAcceleration()
    {
        return _treeSpreadAcceleration;
    }

    public void SetWind(float angle, float speed)
    {
        windAngle = angle;
        windSpeed = speed;
    }
}
