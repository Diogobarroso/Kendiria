using UnityEngine;

public class FlameController : MonoBehaviour
{
    [SerializeField] private LayerMask _treeSpreadAccelerationLayerMask;
    
    [SerializeField] private LayerMask _noBurnLayerMask;

    private Fire _fire;

    private float spreadTime = 0.0f;
    private float animTime = 0.0f;
    private Vector3 _basePosition;
    private float _animOffset;
    private Hose hose;

    public void Initialize(Fire fire)
    {
        _fire = fire;
    }

    private void Start()
    {
        _basePosition = transform.position;
        _animOffset = Random.Range(0.0f, 10.0f);
        hose = GetComponentInChildren<Hose>();
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

        float initialAngle = _fire.windAngle + Random.Range(-35.0f, 35.0f);

        for (float angle = initialAngle; angle < 360.0f + initialAngle; angle += 360.0f/5) // Check an even spacing around the flame
        {
            float windDistanceEffect = Random.Range(_fire.windSpeed / 2, _fire.windSpeed);
            Vector3 flameDisplacement = Quaternion.AngleAxis(angle, Vector3.back) * Vector3.up * (_fire.GetFlameSpreadDistance());
            float windInfluence = (360.0f - Mathf.Abs(_fire.windAngle - angle)) / 360.0f;
            flameDisplacement += flameDisplacement * windDistanceEffect * windInfluence;
            Vector3 newFlamePosition = transform.position + flameDisplacement;

            RaycastHit2D hitInfo = Physics2D.CircleCast(newFlamePosition, 0.5f, Vector2.zero, Mathf.Infinity, _noBurnLayerMask);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out WaterController _))
        {
            hose.HoseBeforeHoes();
            _fire.TryExtinguish(transform);
        }
    }
}
