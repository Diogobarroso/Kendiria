using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private FlameController flamePrefab;
    [SerializeField] private float flameSpreadSpeed;
    [SerializeField] private float flameSpreadDistance;
    [SerializeField] private float flameAnimSpeed;
    [SerializeField] private float flameAnimHeight;

    [HideInInspector] public static List<Transform> flames = new List<Transform>();

    private void Start()
    {
        AddFlame(transform.position);
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
}
