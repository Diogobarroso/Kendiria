using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    Rigidbody2D body;

    private Vector2 inputMove = Vector2.zero;
    private Vector2 inputTurn = Vector2.zero;

    private float turnDir = 0.0f;

    public float moveSpeed = 1.0f;

    public Action<Character> _onDeath;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _onDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        body.velocity = inputMove * moveSpeed;

        if (turnDir != 0.0f)
            transform.rotation = Quaternion.Euler(0, 0, turnDir * Mathf.Rad2Deg);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 inputTurn = context.ReadValue<Vector2>();
        turnDir = Mathf.Atan2(inputTurn.y, inputTurn.x);
    }
}
