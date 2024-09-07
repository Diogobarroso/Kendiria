using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private float hosingSpeed;

    private float hosingTime = 0.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            hosingTime += Time.deltaTime;
            if (hosingTime > 1 / hosingSpeed)
            {
                Instantiate(water, this.transform);
            }
        }
        else
        {
            hosingTime = 0.0f;
        }
    }
}
