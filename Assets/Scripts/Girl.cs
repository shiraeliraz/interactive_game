using System;
using DG.Tweening;
using UnityEngine;

public class Girl : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("idle");
    [SerializeField] private Vector3 nextToMarketPos;
    [SerializeField] private float moveToMarketDuration = 3;
    
    private Animator _animator;

    private void OnEnable()
    {
        GameEvents.AllTomatoesInMarket += MoveToMarket;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        GameEvents.AllTomatoesInMarket -= MoveToMarket;
    }

    private void MoveToMarket()
    {
        transform.DOMove(nextToMarketPos, moveToMarketDuration).SetEase(Ease.Linear).OnComplete(SetIdle);
    }

    private void SetIdle()
    {
        _animator.SetTrigger(Idle);
    }
}
