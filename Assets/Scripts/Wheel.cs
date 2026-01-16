using System;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private float spinDegreesPerSecond;
    private bool _spinning = false;

    private void OnEnable()
    {
        GameEvents.StartDriving += ActivateWheels;
        GameEvents.StopDriving += DeactivateWheels;
    }

    private void OnDisable()
    {
        GameEvents.StartDriving -= ActivateWheels;
        GameEvents.StopDriving -= DeactivateWheels;
    }

    private void ActivateWheels()
    {
        _spinning = true;
    }

    private void DeactivateWheels()
    {
        _spinning = false;
    }
    void Update()
    {
        if (_spinning)
        {
            transform.Rotate(0f, 0f, spinDegreesPerSecond * Time.deltaTime);
        }
    }
        
}
