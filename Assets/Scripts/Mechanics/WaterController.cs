using UnityEngine;

public class WaterController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO is this even right script?
        if (other.TryGetComponent(out Fire fire))
        {
            Destroy(fire.gameObject);
        }
    }
}
