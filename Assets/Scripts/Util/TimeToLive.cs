using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    [SerializeField] private float _timeToLive = 1f;

    private void Start()
    {
        Destroy(gameObject, _timeToLive);
    }
}
