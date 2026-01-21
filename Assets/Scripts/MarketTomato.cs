using System;
using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarketTomato : MonoBehaviour
{
    [Header("Market Settings")]
    [SerializeField] private Vector3 marketTarget;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float duration = 1f;
    
    [Header("Bag Settings")]
    [SerializeField] private GameObject bag;
    [SerializeField] private float jumpPower2 = 2f;
    [SerializeField] private float duration2 = 1f;
    
    [Header("Kitchen Settings")]
    [SerializeField] private Vector3 kitchenTargetPos = new Vector3(85.215f,0.816f,0);
    [SerializeField] private Vector3 kitchenTargetRot = new Vector3(0,0,12.798f);
    [SerializeField] private float jumpPower3 = 2f;
    [SerializeField] private float duration3 = 1f;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip swooshSound;
    [SerializeField] private AudioClip coinSound;


    private bool _atCrate = true;
    private bool _canBeBought = false;
    private bool _canBeAtKitchen = false;
    private bool _inKitchen = false;
    private Tomato _tomatoScript;
    

    private void Update()
    {

        bool pressed =
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

        if (!pressed)
            return;

        Vector2 screenPos =
            Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (_atCrate)
            {
                JumpToMarket();
                return;
            }
            if (_canBeBought)
            {
                JumpToBag();
                return;
            }

            if (_canBeAtKitchen)
            {
                JumpToKitchen();
            }
            
            
        }
    }

    private void OnEnable()
    {
        GameEvents.AllTomatoesInMarket += EnableBuy;
        GameEvents.BoughtTomato += DisableBuy;
        GameEvents.DoorOpened += CanBePutInKitchen;
    }

    private void OnDisable()
    {
        GameEvents.AllTomatoesInMarket -= EnableBuy;
        GameEvents.BoughtTomato -= DisableBuy;
        GameEvents.DoorOpened -= CanBePutInKitchen;
    }

    private void JumpToMarket()
    {
        audioSource.PlayOneShot(swooshSound);
        _atCrate = false;
        transform.DOJump(marketTarget, jumpPower, 1, duration)
            .SetUpdate(true).OnComplete(FinishedJumpToMarket);
        transform.DORotate(Vector3.zero, 0.25f)
            .SetEase(Ease.Linear);
    }
    private void JumpToBag()
    {
        GameEvents.BoughtTomato?.Invoke();
        audioSource.PlayOneShot(swooshSound);
        _atCrate = false;
        transform.parent = bag.transform;
        transform.DOJump(bag.transform.position, jumpPower2, 1, duration2)
            .SetUpdate(true).OnComplete(FinishedJumpToBag);
        transform.DORotate(kitchenTargetRot, 0.25f)
            .SetEase(Ease.Linear);
    }

    private void JumpToKitchen()
    {
        audioSource.PlayOneShot(swooshSound);
        transform.SetParent(null);
        transform.DOJump(kitchenTargetPos, jumpPower, 1, duration)
            .SetUpdate(true).OnComplete(FinishedJumpToKitchen);
    }

    private void FinishedJumpToKitchen()
    {
        if (_inKitchen)
        {
            return;
        }
        GameEvents.TomatoInKitchen?.Invoke();
        Debug.Log("tomato in kitchen");
        _inKitchen = true;
    }

    private void FinishedJumpToMarket()
    {
        GameEvents.TomatoReachedMarket?.Invoke();
    }

    private void FinishedJumpToBag()
    {
        audioSource.PlayOneShot(coinSound);
        GameEvents.TomatoInBag?.Invoke();
    }

    private void EnableBuy()
    {
        _canBeBought = true;
    }

    private void DisableBuy()
    {
        _canBeBought = false;
    }

    private void CanBePutInKitchen()
    {
        _canBeAtKitchen = true;
    }
}
