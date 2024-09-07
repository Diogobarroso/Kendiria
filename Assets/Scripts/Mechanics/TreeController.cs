using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10.0f;
    [SerializeField] private float _burningDamage = 1.0f;
    [SerializeField] private LayerMask _fireLayerMask;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _burntSprite;
    
    private float _currentHealth = 0.0f;
    private bool _isBurning = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (!_isBurning)
            return;

        _currentHealth -= _burningDamage * Time.deltaTime;

        if (_currentHealth <= 0.0f)
        {
            _collider.enabled = false;
            _spriteRenderer.sprite = _burntSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isBurning) return;
        
        if ((_fireLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            _isBurning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isBurning) return;
        
        if ((_fireLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            _isBurning = false;
        }
    }
}
