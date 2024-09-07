using System;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public Rigidbody2D body;

    private Vector2 inputMove = Vector2.zero;
    public Vector2 inputTurn = Vector2.zero;

    public float turnDir = 0.0f;

    public float moveSpeed = 1.0f;

    public Action<Character> _onDeath;
    [SerializeField] private SpriteRenderer playerColor;

    bool isDead = false;

    public Action OnReady;
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        isDead = true;
        _onDeath?.Invoke(this);

        body.isKinematic = true;
        body.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        body.velocity = inputMove * moveSpeed;

        if (turnDir != 0.0f)
            transform.rotation = Quaternion.Euler(0, 0, turnDir * Mathf.Rad2Deg);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
        if(inputMove.magnitude!=0){
            if(!GetComponent<AudioSource>().isPlaying){
                GetComponent<AudioSource>().loop=true;
                GetComponent<AudioSource>().Play();

            }
        }
        else{
            GetComponent<AudioSource>().loop = false;

        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {

        if(context.control.device.description.deviceClass == "Mouse"){
            
            Vector3 mouseRelativeToPlayer = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
            turnDir = Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x);


            return;
        }
        Vector2 inputTurn = context.ReadValue<Vector2>();
        turnDir = Mathf.Atan2(inputTurn.y, inputTurn.x);
    }

    public void OnReadyUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnReady?.Invoke();
        }
    }

    public void SetColor(Color color)
    {
        playerColor.color = color;
    }
}
