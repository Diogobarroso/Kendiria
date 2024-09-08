using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
public class Wave : ScriptableObject
{
    public Vector2[] initialFlamesPosition;
    [Range(0, 10)]
    public float windSpeed;
    public float windAngle;
    public float flameSpreadSpeed;
    [Range(0,100)]
    public float humidity;
}
