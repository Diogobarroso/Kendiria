using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
public class Wave : ScriptableObject
{
    [Range(1,15)]
    public int initialFlames;
    public float windSpeed;
    public Vector2 windDirection;
    public float flameSpreadSpeed;
    [Range(0,100)]
    public float humidity;
}
