using System;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private FlameController flamePrefab;
    [SerializeField] private float flameSpreadSpeed;
    [SerializeField] private float flameSpreadDistance;
    [SerializeField] private float _treeSpreadAcceleration;
    [SerializeField] private float flameAnimSpeed;
    [SerializeField] private float flameAnimHeight;

    [HideInInspector] public List<Transform> flames = new List<Transform>();
    public Action OnFireExtinguished;

    private void Start()
    {
        //AddFlame(transform.position);
    }

    public void TryExtinguish(Transform flame)
    {
        int index = flames.FindIndex(a => a != null && a.transform == flame);
        if (index == -1)
            return;
        flames.RemoveAt(index);
        Destroy(flame.gameObject);

        if (flames.Count == 0)
        {
            OnFireExtinguished?.Invoke();
        }
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
}
