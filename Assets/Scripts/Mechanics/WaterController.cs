using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private float time = 0.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.position += Vector3.down * Time.deltaTime;
    }
}
