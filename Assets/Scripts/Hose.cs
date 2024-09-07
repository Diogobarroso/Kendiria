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

    bool isHoseActive = false;

    [SerializeField] private InputActionAsset controls;

    private void Awake()
    {
        controls.FindActionMap("Player").FindAction("Fire").performed += ctx => isHoseActive = true;
        controls.FindActionMap("Player").FindAction("Fire").canceled += ctx => isHoseActive = false;
    }

    void Update()
    {
        timeSinceLastHose += Time.deltaTime;
        if (isHoseActive)
        {
            HoseBeforeHoes();
        }
    }
    public void HoseBeforeHoes()
    {
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
