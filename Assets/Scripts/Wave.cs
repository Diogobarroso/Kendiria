using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
public class Wave : ScriptableObject
{
    public Vector2[] initialFlamesPosition;
    public float windSpeed;
    public Vector2 windDirection;
    public float flameSpreadSpeed;
    [Range(0,100)]
    public float humidity;
}
