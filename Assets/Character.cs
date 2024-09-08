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

    private Hose hose;

    [SerializeField] private Animator _characterAnimator;
    private enum Orientation
    {
        Front,
        Back,
        Left,
        Right
    }
    private Orientation _orientation = Orientation.Right;

    private void ChangeOrientation()
    {
        Orientation orientation;

        if (turnDir >= -Mathf.PI / 4 && turnDir <= Mathf.PI / 4)
            orientation = Orientation.Right;
        else if (turnDir > Mathf.PI / 4 && turnDir < 3 * Mathf.PI / 4)
            orientation = Orientation.Back;
        else if (turnDir < -Mathf.PI / 4 && turnDir > -3 * Mathf.PI / 4)
            orientation = Orientation.Front;
        else
            orientation = Orientation.Left;
            
        if (_orientation == orientation)
            return;

        _orientation = orientation;

        switch (_orientation)
        {
            case Orientation.Front:
                _characterAnimator.SetTrigger("front");
                break;
            case Orientation.Back:
                _characterAnimator.SetTrigger("back");
                break;
            case Orientation.Left:
                _characterAnimator.SetTrigger("left");
                break;
            case Orientation.Right:
                _characterAnimator.SetTrigger("right");
                break;
            default:
                Debug.LogError("HOOOOOOOOOOW?!");
                break;
        }
    }
    
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
        hose = GetComponentInChildren<Hose>();
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        body.velocity = inputMove * moveSpeed;

        if (turnDir != 0.0f) {
            hose.transform.rotation = Quaternion.Euler(0, 0, turnDir * Mathf.Rad2Deg);
            ChangeOrientation();
        }
        //    transform.rotation = Quaternion.Euler(0, 0, turnDir * Mathf.Rad2Deg);
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
