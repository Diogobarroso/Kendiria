using UnityEngine;

public class FlameController : MonoBehaviour
{
    [SerializeField] private LayerMask _treeSpreadAccelerationLayerMask;

    private Fire _fire;

    private float spreadTime = 0.0f;
    private float animTime = 0.0f;
    private Vector3 _basePosition;
    private float _animOffset;

    public void Initialize(Fire fire)
    {
        _fire = fire;
    }

    private void Start()
    {
        _basePosition = transform.position;
        _animOffset = Random.Range(0.0f, 10.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        // Flame animation
        animTime += Time.deltaTime;
        transform.position = new Vector2(_basePosition.x , _basePosition.y + Mathf.Abs(_fire.GetFlameAnimHeight() * Mathf.Sin(_fire.GetFlameAnimSpeed() * (animTime + _animOffset))));

        // Flame spread
        spreadTime += Time.deltaTime * CalculateSpreadSpeed();

        if (spreadTime < 1.0f) return;

        float initialAngle = Random.Range(0.0f, 70.0f);

        for (float angle = initialAngle; angle < 360.0f + initialAngle; angle += 360.0f/5) // Check an even spacing around the flame
        {
            Vector3 newFlamePosition = transform.position + Quaternion.AngleAxis(angle, Vector3.back) * Vector3.up * _fire.GetFlameSpreadDistance();
            LayerMask fireLayerMask = LayerMask.GetMask("Fire");
            RaycastHit2D hitInfo = Physics2D.CircleCast(newFlamePosition, 0.5f, Vector2.zero, Mathf.Infinity, fireLayerMask);

            if (hitInfo.collider == null)
            {
                _fire.AddFlame(newFlamePosition);
            }
        }
        
        spreadTime = 0.0f;
    }

    private float CalculateSpreadSpeed()
    {
        float currentSpreadSpeed = _fire.GetFlameSpreadSpeed();

        RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, 0.5f, Vector2.zero, Mathf.Infinity, _treeSpreadAccelerationLayerMask);
        if (hitInfo.collider != null)
        {
            currentSpreadSpeed *= _fire.GetTreeSpreadAcceleration();
        }

        return currentSpreadSpeed;
    }
}
