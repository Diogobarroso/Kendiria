using UnityEngine;

public class WaterController : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 baseSpeed;
    [HideInInspector] public Fire fire;
    [SerializeField] private float waterSpeed;

    private void Update()
    {
        transform.position += baseSpeed * Time.deltaTime + direction * waterSpeed * Time.deltaTime;
    }
}
