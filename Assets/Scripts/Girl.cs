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
    [SerializeField] private Vector3 leaveKitchenLocation;
    [SerializeField] private float leaveKitchenWalkDuration;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField] private AudioClip walkingSound;
    
    private Animator _animator;

    private void OnEnable()
    {
        GameEvents.AllTomatoesInMarket += MoveToMarket;
        GameEvents.TomatoInBag += GoHome;
        GameEvents.DoorOpened += GoThroughDoor;
        GameEvents.TomatoInKitchen += LeaveKitchen;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        GameEvents.AllTomatoesInMarket -= MoveToMarket;
        GameEvents.TomatoInBag -= GoHome;
        GameEvents.DoorOpened -= GoThroughDoor;
        GameEvents.TomatoInBag -=  LeaveKitchen;
    }

    private void MoveToMarket()
    {
        transform.DOMove(nextToMarketPos, moveToMarketDuration).SetEase(Ease.Linear).OnComplete(SetIdle);
    }

    private void SetIdle()
    {
        _animator.SetTrigger(Idle);
        StopFootsteps();
    }

    private void SetWalking()
    {
        _animator.SetTrigger(Walking);
        StartFootsteps();
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

    private void LeaveKitchen()
    {
        SetWalking();
        transform.DOMove(leaveKitchenLocation, leaveKitchenWalkDuration).SetEase(Ease.Linear).OnComplete(SetIdle);
    }
    
    private void StartFootsteps()
    {
        if (walkingSound == null || footStepSource == null) return;

        footStepSource.clip = walkingSound;
        footStepSource.loop = true;
        footStepSource.Play();
    }

    private void StopFootsteps()
    {
        if (footStepSource == null) return;

        footStepSource.Stop();
    }
}
