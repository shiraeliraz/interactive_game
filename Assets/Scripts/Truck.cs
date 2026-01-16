using DG.Tweening;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] private GameObject parentTruck;
    private bool _activated = false;
    private Animator _animator;

    private void OnEnable()
    {
        GameEvents.StopDriving +=  StopDriving;
    }

    private void OnDisable()
    {
        GameEvents.StopDriving -= StopDriving;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void ActivateWheels()
    {
        if (_activated)
        {
            return;
        }
        _activated = true;
        GameEvents.StartDriving?.Invoke();
    }

    private void StopDriving()
    {
        _animator.Play("truckStop");
        
    }
}
