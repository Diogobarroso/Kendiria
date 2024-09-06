using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float flameAnimSpeed;
    [SerializeField] private float flameAnimHeight;
    [SerializeField] public Transform[] Flames;

    private float time = 0.0f;

    private void Update()
    {
        time += Time.deltaTime;

        foreach (Transform flame in Flames)
        {
            flame.position = new Vector2(0.0f, Mathf.Abs(flameAnimHeight * Mathf.Sin(flameAnimSpeed * time)));
        }
    }
}
