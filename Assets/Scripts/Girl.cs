using System;
using DG.Tweening;
using UnityEngine;

public class Girl : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Walking = Animator.StringToHash("walking");
    [SerializeField] private Vector3 nextToMarketPos;
    [SerializeField] private float moveToMarketDuration = 3;
    [SerializeField] private Vector3 kitchenLocation;
    [SerializeField] private float kitchenWalkDuration;
    [SerializeField] private Vector3 afterDoorLocation;
    [SerializeField] private float afterDoorWalkDuration;
    
    private Animator _animator;

    private void OnEnable()
    {
        GameEvents.AllTomatoesInMarket += MoveToMarket;
        GameEvents.TomatoInBag += GoHome;
        GameEvents.DoorOpened += GoThroughDoor;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        GameEvents.AllTomatoesInMarket -= MoveToMarket;
        GameEvents.TomatoInBag -= GoHome;
        GameEvents.DoorOpened -= GoThroughDoor;
    }

    private void MoveToMarket()
    {
        transform.DOMove(nextToMarketPos, moveToMarketDuration).SetEase(Ease.Linear).OnComplete(SetIdle);
    }

    private void SetIdle()
    {
        _animator.SetTrigger(Idle);
    }

    private void SetWalking()
    {
        _animator.SetTrigger(Walking);
    }

    private void GoHome()
    {
        SetWalking();
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);
        GameEvents.CameraGlide?.Invoke();
        transform.DOMove(kitchenLocation, kitchenWalkDuration).SetEase(Ease.Linear).OnComplete(SetIdle);
        
    }

    private void GoThroughDoor()
    {
        SetWalking();
        transform.DOMove(afterDoorLocation, afterDoorWalkDuration).SetEase(Ease.Linear).OnComplete(SetIdle);
        
    }
}
