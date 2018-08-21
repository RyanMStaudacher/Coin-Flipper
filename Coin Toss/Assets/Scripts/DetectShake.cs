using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectShake : MonoBehaviour
{
    public static event UnityAction hasBeenShook;

    private Vector3 lowPassValue;
    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float shakeDetectionThreshold = 2.0f;
    private float lowPassFilterFactor;
    private bool isShaking = false;


	// Use this for initialization
	void Start ()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if(deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            if(!isShaking)
            {
                if (hasBeenShook != null)
                {
                    hasBeenShook.Invoke();
                }
                isShaking = true;
            }
        }
        else if(deltaAcceleration.sqrMagnitude < shakeDetectionThreshold)
        {
            isShaking = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (hasBeenShook != null)
            {
                hasBeenShook.Invoke();
            }
        }
	}
}
