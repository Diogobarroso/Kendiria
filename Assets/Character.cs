using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    Rigidbody2D body;

    /*
    float horizontal;
    float vertical;

    public UnityEvent<Vector2> leftStick;
    public UnityEvent<Vector2> rightStick;

    Vector2 lookAtPos;
    */

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 inputVector = Vector2.zero;

    public float moveSpeed = 1.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        //lookAtPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        //transform.LookAt(lookAtPos, transform.up);

        moveDirection = transform.TransformDirection(inputVector);
        moveDirection *= moveSpeed;
        body.velocity = moveDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookAtPos = context.ReadValue<Vector2>();
        float dir = Mathf.Atan2(lookAtPos.y, lookAtPos.x);
        transform.rotation = Quaternion.Euler(0, 0, dir * Mathf.Rad2Deg);
    }
}
