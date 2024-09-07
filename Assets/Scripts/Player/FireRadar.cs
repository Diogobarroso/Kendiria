using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireRadar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private LevelManager lvlManager;
    private List<Transform> flames = new();

    // Start is called before the first frame update
    void Start()
    {
        // TODO No time or patient to do this better :)
        lvlManager = GameObject.Find("LevelManager")?.GetComponent<LevelManager>();
        _spriteRenderer.enabled = false;
        
        lvlManager.OnWaveStart += OnWaveStart;
    }

    private void OnWaveStart(List<Transform> list)
    {
        flames = list.OrderBy(f => Vector3.Distance(f.position, transform.parent.position)).ToList();
        _spriteRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flames.Count == 0)
            return;

        Vector2 direction = (flames[0].position - transform.parent.position).normalized;
        // TODO I don't have enough energy to make this magic values into configurable variables
        transform.position = (Vector2) transform.parent.position + direction * 0.75f;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}
