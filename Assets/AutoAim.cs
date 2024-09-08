using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class AutoAim : MonoBehaviour
{
    public bool isActive = false;
    private Character _char;
    private LevelManager lvlManager;

    private float _timeSinceLastToggle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _char = GetComponent<Character>();
        lvlManager = GameObject.Find("LevelManager")?.GetComponent<LevelManager>();
    }

    public void ToggleAutoAim(InputAction.CallbackContext context)
    {
        if (_timeSinceLastToggle < 2f)
            return;
        isActive = !isActive;
        _timeSinceLastToggle = 0.0f;
        Debug.Log("AUTO AIM IS " + isActive);
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastToggle += Time.deltaTime;
        if (isActive && lvlManager._currentWaveFire != null)
        {
            Transform[] flames = lvlManager._currentWaveFire?.flames.ToArray();

            flames = flames.OrderBy(f => Vector3.Distance(f.position, _char.transform.position)).ToArray();

            Vector2 turn = new Vector2(flames[0].position.x - _char.transform.position.x, flames[0].position.y - _char.transform.position.y).normalized;

            _char.turnDir = Mathf.Atan2(turn.y, turn.x);
        }
    }
}
