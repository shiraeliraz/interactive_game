using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crate : MonoBehaviour
{
    [SerializeField] private Vector3 tomatoLoadLocation;
    [SerializeField] private float firstMoveDuration = 1f;
    [SerializeField] private GameObject truck;
    [SerializeField] private Collider2D truckCollider;

    private int _tomatoCounter;
    private Collider2D _collider;


    private void OnEnable()
    {
        transform.DOMove(tomatoLoadLocation, firstMoveDuration);
        GameEvents.TomatoInCrate += GainTomato;
        _collider =  GetComponent<Collider2D>();
        _collider.enabled = false;
        GameEvents.StopDriving += OnTruckStopped;

    }

    private void OnDisable()
    {
        GameEvents.TomatoInCrate -= GainTomato;
        GameEvents.StopDriving -= OnTruckStopped;
    }

    private void GainTomato()
    {
        _tomatoCounter++;
        if (_tomatoCounter == 5)
        {
            GameEvents.CameraGlide.Invoke();
            _collider.enabled = true;
            truckCollider.enabled = false;
        }
    }

    public void ReachTruck()
    {
        truckCollider.enabled = true;
        GameEvents.CrateReachedTruck?.Invoke();
        _collider.enabled = false;
    }

    private void OnTruckStopped()
    {
        _collider.enabled = true;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void CrateReachedFloor()
    {
        GameEvents.CrateReachedFloor?.Invoke();
    }

}