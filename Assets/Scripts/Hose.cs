using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hose : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private float hosingSpeed;
    [SerializeField] private float spreadAngle;
    [SerializeField] private AudioSource hoseStartSFX;
    [SerializeField] private AudioSource hoseWaterSFX;


    private float hosingTime = 0.0f;

    bool isHoseActive = false;

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
            if (hoseWaterSFX != null)
                hoseWaterSFX.Stop();
        }
    }

    public void OnFireHose(InputAction.CallbackContext context)
    {
        isHoseActive = context.ReadValueAsButton();
    }

    public void HoseBeforeHoes()
    {
        if(hoseStartSFX != null && hosingTime == 0){
            hoseStartSFX.Play();
        }
        
        hosingTime += Time.deltaTime;

        if (hosingTime > 1 / hosingSpeed)
        {
            if(hoseWaterSFX != null && !hoseWaterSFX.isPlaying)
            {
                hoseWaterSFX.Play();
            }
            GameObject newWater = Instantiate(water);
            newWater.transform.position = this.transform.position;
            newWater.transform.rotation = this.transform.parent.rotation;
            newWater.GetComponent<WaterController>().direction = Quaternion.AngleAxis(Random.Range(-spreadAngle * 0.5f, spreadAngle * 0.5f), Vector3.back) * this.transform.right;
            if (this.transform.parent.GetComponent<Character>() != null)
                newWater.GetComponent<WaterController>().baseSpeed = this.transform.parent.GetComponent<Character>().body.velocity;
        }
    }
}
