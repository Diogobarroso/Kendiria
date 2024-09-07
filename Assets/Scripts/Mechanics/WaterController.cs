using UnityEngine;

public class WaterController : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 baseSpeed;
    [HideInInspector] public Fire fire;
    [SerializeField] private float waterSpeed;
    [SerializeField] private float lifeTime;

    private float time = 0.0f;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.position += baseSpeed * Time.deltaTime + direction * waterSpeed * Time.deltaTime;
    }
}
