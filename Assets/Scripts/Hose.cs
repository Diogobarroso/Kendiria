using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hose : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private float hosingSpeed;

    private float hosingTime = 0.0f;

    bool isHoseActive = false;

    [SerializeField] private InputActionAsset controls;

    private Vector3 lastFramePosition;

    private void Awake()
    {
    }

    void Update()
    {
        if (isHoseActive)
        {
            HoseBeforeHoes();
        }
        else
        {
            hosingTime = 0.0f;
        }

        lastFramePosition = this.transform.position;
    }

    public void OnFireHose(InputAction.CallbackContext context)
    {
        isHoseActive = context.ReadValueAsButton();
    }

    public void HoseBeforeHoes()
    {
        hosingTime += Time.deltaTime;

        if (hosingTime > 1 / hosingSpeed)
        {
            GameObject newWater = Instantiate(water);
            newWater.transform.position = this.transform.position;
            newWater.transform.rotation = this.transform.parent.rotation;
            newWater.GetComponent<WaterController>().direction = this.transform.parent.right;
            newWater.GetComponent<WaterController>().baseSpeed = this.transform.parent.GetComponent<Character>().body.velocity;
        }
    }
}
