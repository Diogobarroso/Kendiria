using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    Vector2 lookAtPos;

    public float runSpeed = 1.0f;

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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        lookAtPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        transform.LookAt(lookAtPos,transform.up);
    }
}
