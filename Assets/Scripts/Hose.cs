using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hose : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private float hosingSpeed;

    private float hosingTime = 0.0f;
    private float timeSinceLastHose = 0.0f;

    [SerializeField] private InputActionAsset controls;

    private void Awake()
    {
        controls.FindActionMap("Player").FindAction("Fire").performed += ctx => HoseBeforeHoes();
    }

    void Update()
    {
        timeSinceLastHose += Time.deltaTime;
        /*
        if (Input.GetKey(KeyCode.Mouse0))
        {
            HoseBeforeHoes();
        }*/
    }
    
    public void HoseBeforeHoes()
    {
        Debug.Log("WHERE MY HOES AT");
        if (timeSinceLastHose > 0.5f)
        {
            hosingTime = 0.0f;
        }
        hosingTime += Time.deltaTime;
        timeSinceLastHose = 0.0f;

        if (hosingTime > 1 / hosingSpeed)
        {
            Instantiate(water, this.transform);
        }
    }
}
